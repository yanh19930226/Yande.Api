using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;




using Microsoft.Extensions.Options;
using SignApi.Commons;

namespace SignApi.SecurityAuthorization.RsaChecker
{
    public class AuthSecurityRsaAuthenticationHandler : AuthenticationHandler<AuthSecurityRsaOptions>
    {

        #region Private
        private readonly ConcurrentDictionary<string, object> _repeatRequestMap =
           new ConcurrentDictionary<string, object>();

        private async Task<AuthenticateResult> AuthenticateResultFailAsync(string message)
        {
            Response.StatusCode = 401;
            await Response.WriteAsync(message);
            return AuthenticateResult.Fail(message);
        } 
        #endregion

        public AuthSecurityRsaAuthenticationHandler(IOptionsMonitor<AuthSecurityRsaOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                string authorization = Request.Headers["AuthSecurity-Authorization"];
                if (string.IsNullOrWhiteSpace(authorization))
                    return AuthenticateResult.NoResult();

                var authorizationSplit = authorization.Split('.');
                if (authorizationSplit.Length != 4)
                    return await AuthenticateResultFailAsync("签名参数不正确");

                //0-9数字,大小写英文字母1到40位正则校验规则
                var reg = new Regex(@"[0-9a-zA-Z]{1,40}");

                #region appId参数校验
                //appId参数校验
                var appid = authorizationSplit[1];
                if (string.IsNullOrWhiteSpace(appid) || !reg.IsMatch(appid))
                    return await AuthenticateResultFailAsync("应用Id不正确");
                #endregion

                #region timestamp参数校验
                //如果不能转成long
                var timeStamp = authorizationSplit[2];
                if (string.IsNullOrWhiteSpace(timeStamp) || !long.TryParse(timeStamp, out var timestamp))
                    return await AuthenticateResultFailAsync("请求时间不正确");
                //请求时间大于5分钟的就抛弃
                if (Math.Abs(UtcTime.CurrentTimeMillis() - timestamp) > 5 * 60 * 1000)
                    return await AuthenticateResultFailAsync("请求已过期");
                #endregion

                #region timestamp+nonce(requestId校验)
                //重放攻击
                //虽然解决了请求参数被篡改的隐患，但是还存在着重复使用请求参数伪造二次请求的隐患。timestamp + nonce方案
                //nonce指唯一的随机字符串，用来标识每个被签名的请求。通过为每个请求提供一个唯一的标识符，服务器能够防止请求被多次使用（记录所有用过的nonce以阻止它们被二次使用）。
                //然而，对服务器来说永久存储所有接收到的nonce的代价是非常大的。可以使用timestamp来优化nonce的存储。
                //假设允许客户端和服务端最多能存在15分钟的时间差，同时追踪记录在服务端的nonce集合。
                //当有新的请求进入时，首先检查携带的timestamp是否在15分钟内，如超出时间范围，则拒绝，然后查询携带的nonce，如存在已有集合，则拒绝。否则，记录该nonce，并删除集合内时间戳大于15分钟的nonce（可以使用redis的expire，新增nonce的同时设置它的超时失效时间为15分钟
                var requestId = authorizationSplit[0];
                if (string.IsNullOrWhiteSpace(requestId) || !reg.IsMatch(requestId))
                    return await AuthenticateResultFailAsync("请求Id不正确"); 
                #endregion

                //sign是空签名参数不正确
                var sign = authorizationSplit[3];
                if (string.IsNullOrWhiteSpace(sign))
                    return await AuthenticateResultFailAsync("签名参数不正确");

                //数据库获取
                var app = AppCallerStorage.ApiCallers.FirstOrDefault(o => o.Id == appid);
                if (app == null)
                    return AuthenticateResult.Fail("未找到对应的应用信息");

                //获取请求体
                var body = await Request.RequestBodyAsync();

                //验证签名
                if (!RsaFunc.ValidateSignature(app.PublickKey, $"{requestId}{appid}{timeStamp}{body}", sign))
                    return await AuthenticateResultFailAsync("签名失败");
                var repeatKey = $"AuthSecurityRequestDistinct:{appid}:{requestId}";
                //自行替换成缓存或者redis本项目不带删除key功能没有过期时间原则上需要设置1小时过期,前后30分钟服务器时间差
                if (_repeatRequestMap.ContainsKey(repeatKey) || !_repeatRequestMap.TryAdd(repeatKey, null))
                {
                    return await AuthenticateResultFailAsync("请勿重复提交");
                }

                #region 所有验证成功登入成功

                //给Identity赋值
                var identity = new ClaimsIdentity(AuthSecurityScheme.AuthenticationSchemeSecurityRsaAuth);
                identity.AddClaim(new Claim("appid", appid));
                identity.AddClaim(new Claim("appname", app.Name));
                identity.AddClaim(new Claim("role", "app"));

                var principal = new ClaimsPrincipal(identity);
                return HandleRequestResult.Success(new AuthenticationTicket(principal, new AuthenticationProperties(), Scheme.Name));

                #endregion
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "RSA签名失败");
                return await AuthenticateResultFailAsync("认证失败");
            }
        }
    }
}
