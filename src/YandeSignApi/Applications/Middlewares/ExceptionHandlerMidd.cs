using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using YandeSignApi.Applications.Logs;
using YandeSignApi.Models.Dtos;

namespace YandeSignApi.Applications.Middlewares
{
    /// <summary>
    /// 异常错误统一返回记录
    /// </summary>
    public class ExceptionHandlerMidd
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// ExceptionHandlerMidd
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMidd(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            if (ex == null) return;
            NLogUtil.WriteAll(NLog.LogLevel.Error, LogType.Web, "全局捕获异常", "全局捕获异常", new Exception("全局捕获异常", ex));
            await WriteExceptionAsync(context, ex).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception e)
        {
            if (e is UnauthorizedAccessException) context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else if (e is Exception) context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json";
            var jm = new CallBackResult();
            jm.Failed(e);
            await context.Response.WriteAsync(JsonConvert.SerializeObject(jm)).ConfigureAwait(false);
        }
    }
}
