using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

namespace In66.Middleware
{
    ///// <summary>
    ///// 中间件
    ///// 记录请求和响应数据
    ///// </summary>
    //public class RequRespLogMildd
    //{
    //    /// <summary>
    //    /// RequestDelegate
    //    /// </summary>
    //    private readonly RequestDelegate _next;
    //    /// <summary>
    //    /// RequRespLogMildd
    //    /// </summary>
    //    /// <param name="next"></param>
    //    public RequRespLogMildd(RequestDelegate next)
    //    {
    //        _next = next;
    //    }
    //    /// <summary>
    //    /// InvokeAsync
    //    /// </summary>
    //    /// <param name="context"></param>
    //    /// <returns></returns>
    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        if (AppSettingsConstVars.MiddlewareRequestResponseLogEnabled)
    //        {
    //            // 过滤只有接口
    //            var api = context.Request.Path.ObjectToString().TrimEnd('/');

    //            var ignoreApis = AppSettingsConstVars.MiddlewareRequestResponseLogIgnoreApis;

    //            if (api.Contains("api") && !ignoreApis.Contains(api) && !api.Contains("FileToStream"))
    //            {
    //                context.Request.EnableBuffering();
    //                Stream originalBody = context.Response.Body;
    //                try
    //                {
    //                    // 存储请求数据
    //                    await RequestDataLog(context);

    //                    using (var ms = new MemoryStream())
    //                    {
    //                        context.Response.Body = ms;

    //                        await _next(context);

    //                        // 存储响应数据
    //                        ResponseDataLog(context.Response, ms);

    //                        ms.Position = 0;
    //                        await ms.CopyToAsync(originalBody);
    //                    }
    //                }
    //                catch (Exception)
    //                {
    //                    // 记录异常
    //                    //ErrorLogData(context.Response, ex);
    //                }
    //                finally
    //                {
    //                    context.Response.Body = originalBody;
    //                }
    //            }
    //            else
    //            {
    //                await _next(context);
    //            }
    //        }
    //        else
    //        {
    //            await _next(context);
    //        }
    //    }

    //    private async Task RequestDataLog(HttpContext context)
    //    {
    //        var request = context.Request;
    //        var sr = new StreamReader(request.Body);
    //        string jwtStr = context.Request.Headers["Authorization"].ObjectToString().Replace("Bearer ", "");

    //        object SysUserId = string.Empty;

    //        if (string.IsNullOrEmpty(jwtStr) == false)
    //        {
    //            var jwtHandler = new JwtSecurityTokenHandler();
    //            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
    //            jwtToken.Payload.TryGetValue("SysUserId", out SysUserId);
    //        }

    //        var content = $"SysUserId:{SysUserId.ToString()}\r\n Route:{request.Path + request.QueryString}\r\n BodyData:{await sr.ReadToEndAsync()}";

    //        if (!string.IsNullOrEmpty(content))
    //        {
    //            Parallel.For(0, 1, e =>
    //            {
    //                LogLockHelper.OutSql2Log("RequestResponseLog", "RequestResponseLog" + DateTime.Now.ToString("yyyy-MM-dd-HH"), new string[] { "Request Data:", content });

    //            });

    //            request.Body.Position = 0;
    //        }
    //    }

    //    private void ResponseDataLog(HttpResponse response, MemoryStream ms)
    //    {
    //        ms.Position = 0;
    //        var ResponseBody = new StreamReader(ms).ReadToEnd();
    //        // 去除 Html
    //        var reg = "<[^>]+>";
    //        var isHtml = Regex.IsMatch(ResponseBody, reg);
    //        if (!string.IsNullOrEmpty(ResponseBody))
    //        {
    //            Parallel.For(0, 1, e =>
    //            {
    //                LogLockHelper.OutSql2Log("RequestResponseLog", "RequestResponseLog" + DateTime.Now.ToString("yyyy-MM-dd-HH"), new string[] { "Response Data:", ResponseBody });
    //            });
    //        }
    //    }
    //}
}
