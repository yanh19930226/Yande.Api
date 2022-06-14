using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YandeSignApi.Applications.Commons;
using YandeSignApi.Applications.Extensions;
using YandeSignApi.Applications.Redis;
using YandeSignApi.Services;

namespace YandeSignApi.Applications.SecurityAuthorization.RsaChecker
{
    public class AuthSecurityRsaAuthenticationHandler : AuthenticationHandler<AuthSecurityRsaOptions>
    {

        #region Private
        private readonly IRedisOperationRepository _redisManager;

        private async Task<AuthenticateResult> AuthenticateResultFailAsync(string message)
        {
            Response.StatusCode = 401;
            await Response.WriteAsync(message);
            return AuthenticateResult.Fail(message);
        }
        #endregion

        public AuthSecurityRsaAuthenticationHandler(
            IRedisOperationRepository redisManager,
            IOptionsMonitor<AuthSecurityRsaOptions> options,
            ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock
            ) : base(options, logger, encoder, clock)
        {
            _redisManager = redisManager;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                //0-9数字,大小写英文字母1到40位正则校验规则
                var reg = new Regex(@"[0-9a-zA-Z]{1,40}");

                #region appId参数校验
                //appId参数校验
                var appid = Request.Headers["appid"];
                if (string.IsNullOrWhiteSpace(appid) || !reg.IsMatch(appid))
                    return await AuthenticateResultFailAsync("应用Id不正确");
                #endregion

                #region timestamp参数校验
                //如果不能转成long
                var timeStamp = Request.Headers["timestamp"];
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
                //当有新的请求进入时，首先检查携带的timestamp是否在15分钟内，如超出时间范围，则拒绝，然后查询携带的nonce，如存在已有集合，则拒绝。
                //否则，记录该nonce，并删除集合内时间戳大于15分钟的nonce（可以使用redis的expire，新增nonce的同时设置它的超时失效时间为15分钟

                //使用nonce和timestamp解决重发攻击
                //nonce和timestamp，而以熟知的web应用来举例。首先，客户端请求前生成一个随机数nonce，作为该请求的唯一标识，签名时加入该nonce以保证nonce不被篡改，同时由于签名中加入了随机数nonce，因此相同的请求参数，每次签名得出的结果都不同，保证了签名不可预测，nonce放在header中一起发送给服务端，以便服务端使用该nonce进行验签，并记录起来。
                //后续请求应该判断该nonce是否使用过，若已使用过，则认为是重发攻击，这样就可以避免同一个请求被重发多次。
                //但是，服务器记录的nonce会随着请求次数的增多而增多，客户端生成重复的nonce的概率也会增高，一些正常的请求可能会被误判为重发攻击。此时有必要为nonce设置一个有效期，比如60秒，超过60秒就删除。但是，问题又来了，因为服务器只保留60秒内的nonce，则一个已发送的请求在60秒后又可以重发了。因此还需要加入一个时间戳timestamp参数用于表示请求时间，同样需要加入到签名中以保证timestamp不被篡改，服务器判断请求时间如果在60秒以前，则认为这是一个过时的请求，抛出异常。
                //因此，服务器只需要记录60秒以内的nonce并拒绝60秒以前的请求就可以确保没有请求被重发了

                var nonce = Request.Headers["nonce"];
                if (string.IsNullOrWhiteSpace(nonce) || !reg.IsMatch(nonce))
                    return await AuthenticateResultFailAsync("请求Id不正确");

                var redisKey = $"AuthSecurityRequestDistinct:{timestamp}:{appid}:{nonce}";

                if (!string.IsNullOrEmpty(_redisManager.Get(redisKey).Result))
                {
                    return await AuthenticateResultFailAsync("请勿重复提交");
                }
                else
                {
                    _redisManager.Set(redisKey, nonce, TimeSpan.FromMinutes(5));
                }

                #endregion

                #region sign参数校验
                //sign是空签名参数不正确
                var signature = Request.Headers["signature"];
                if (string.IsNullOrWhiteSpace(signature))
                    return await AuthenticateResultFailAsync("签名参数不正确");
                #endregion

                #region 应用信息校验
                //数据库获取
                var app = AppCallerStorage.ApiCallers.FirstOrDefault(o => o.Id == appid);
                if (app == null)
                    return AuthenticateResult.Fail("未找到对应的应用信息");
                #endregion

                #region signature验证

                var bodyString = await Request.RequestBodyAsync();

                var uri = Request.Path;

                var method = Request.Method;

                string signBodyString = $"{method}\n{uri}\n{timestamp}\n{nonce}\n{bodyString}\n";

                if (!SecurityFunc.ValidateSignature(app.PublickKey, $"{signBodyString}", signature))
                    return await AuthenticateResultFailAsync("签名失败");
                #endregion

                #region 所有验证成功登入成功

                //给Identity赋值使用AuthenticationSchemeSecurityRsaAuth
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
