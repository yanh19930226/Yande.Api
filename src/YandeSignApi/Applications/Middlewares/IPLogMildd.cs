﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using YandeSignApi.Applications.Commons;
using YandeSignApi.Applications.Extensions;
using YandeSignApi.Applications.Logs;

namespace YandeSignApi.Applications.Middlewares
{
    /// <summary>
    /// 中间件
    /// 记录IP请求数据
    /// </summary>
    public class IPLogMildd
    {
        /// <summary>
        ///
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        ///
        /// </summary>
        /// <param name="next"></param>
        public IPLogMildd(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// InvokeAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (AppSettingsConstVars.MiddlewareIpLogEnabled)
            {
                // 过滤，只有接口
                if (context.Request.Path.Value != null && (context.Request.Path.Value.Contains("api") || context.Request.Path.Value.Contains("Api")))
                {
                    context.Request.EnableBuffering();
                    try
                    {
                        // 存储请求数据
                        var dt = DateTime.Now;
                        var request = context.Request;
                        var requestInfo = JsonConvert.SerializeObject(new RequestInfo()
                        {
                            Ip = GetClientIp(context),
                            Url = request.Path.ObjectToString().TrimEnd('/').ToLower(),
                            Datetime = dt.ToString("yyyy-MM-dd HH:mm:ss"),
                            Date = dt.ToString("yyyy-MM-dd"),
                            Week = Util.GetWeek(),
                            RequestMethod = request.Method,
                            Agent = request.Headers["User-Agent"].ObjectToString()
                        });

                        if (!string.IsNullOrEmpty(requestInfo))
                        {
                            Parallel.For(0, 1, e =>
                            {
                                LogLockHelper.OutSql2Log("RequestIpInfoLog", "RequestIpInfoLog" + dt.ToString("yyyy-MM-dd-HH"), new string[] { requestInfo + "," }, false);
                            });
                            request.Body.Position = 0;
                        }
                        await _next(context);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    await _next(context);
                }
            }
            else
            {
                await _next(context);
            }
        }
        /// <summary>
        /// GetClientIp
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetClientIp(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].ObjectToString();
            if (string.IsNullOrEmpty(ip))
            {
                if (context.Connection.RemoteIpAddress != null)
                {
                    ip = context.Connection.RemoteIpAddress.MapToIPv4().ObjectToString();
                }
            }
            return ip;
        }

    }
}
