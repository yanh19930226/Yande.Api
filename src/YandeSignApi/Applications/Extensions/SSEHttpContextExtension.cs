using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace YandeSignApi.Applications.Extensions
{
    public static class SSEHttpContextExtension
    {
        /// <summary>
        /// 自定义的SSE消息对象实体
        /// </summary>
        public class SseMessageObject
        {
            public string MsgId { get; set; }
            public string MsgData { get; set; }
        }

        //public static HttpResponseMessage BuildingSse(HttpRequestMessage request)
        //{

        //    System.Net.Http.HttpResponseMessage response = request.CreateResponse();
        //    response.Content = new System.Net.Http.PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteToStream, new MediaTypeHeaderValue("text/event-stream"));
        //    return response;
        //}

        //private static readonly ConcurrentDictionary<StreamWriter, StreamWriter> _streammessage = new ConcurrentDictionary<StreamWriter, StreamWriter>();
        //public static void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        //{
        //    StreamWriter streamwriter = new StreamWriter(outputStream);
        //    _streammessage.TryAdd(streamwriter, streamwriter);
        //}

        public static async Task SSEInitAsync(this HttpContext ctx)
        {
            ctx.Response.Headers.Add("Content-Type", "text/event-stream");
            await ctx.Response.Body.FlushAsync();
        }

        public static async Task SSESendCloseEventAsync(this HttpContext ctx)
        {
            await ctx.Response.WriteAsync("event: close\n");
            await ctx.Response.WriteAsync("retry: 3\n");
            await ctx.Response.WriteAsync("data: bye\n");

            await ctx.Response.WriteAsync("\n");
            await ctx.Response.Body.FlushAsync();
        }
    }
}
