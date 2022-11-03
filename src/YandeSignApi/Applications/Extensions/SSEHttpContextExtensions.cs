namespace YandeSignApi.Applications.Extensions
{
    /// <summary>
    /// 自定义的SSE消息对象实体
    /// </summary>
    public class SseMessageObject
    {
        public string MsgId { get; set; }
        public string MsgData { get; set; }
    }

    /// <summary>
    /// SSEHttpContextExtensions
    /// </summary>
    public static class SSEHttpContextExtensions
    {

        private static readonly ConcurrentDictionary<StreamWriter, StreamWriter> _streammessage = new ConcurrentDictionary<StreamWriter, StreamWriter>();

        // 设置向浏览器推送的消息内容
        private static void MessageCallback(SseMessageObject sseMsg)
        {
            foreach (var subscriber in _streammessage.ToArray())
            {
                try
                {
                    subscriber.Value.WriteLine(string.Format("id: {0}\n", sseMsg.MsgId));
                    subscriber.Value.WriteLine(string.Format("data: {0}\n\n", sseMsg.MsgData));
                    subscriber.Value.Flush();
                }
                catch
                {
                    StreamWriter streamWriter;
                    _streammessage.TryRemove(subscriber.Value, out streamWriter);
                }
            }
        }

        /// <summary>
        /// SSEInitAsync
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static async Task SSEInitAsync(this HttpContext ctx)
        {
            ctx.Response.Headers.Add("Content-Type", "text/event-stream");
            ctx.Response.Headers.Add("Connection", "keep-alive");
            ctx.Response.Headers.Add("Cache-Control", "no-cache");
            await ctx.Response.Body.FlushAsync();
        }

        /// <summary>
        /// SSESendDataAsync
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static async Task SSESendDataAsync(this HttpContext ctx, string msg)
        {
            await ctx.Response.WriteAsync("id: " + Guid.NewGuid().ToString() + "\n");
            await ctx.Response.WriteAsync("event: SetBusinessSettlerEvent\n");
            await ctx.Response.WriteAsync("retry: 3\n");
            await ctx.Response.WriteAsync("data: " + msg + "\n\n");
            await ctx.Response.Body.FlushAsync();
        }

        /// <summary>
        /// SSESendCloseEventAsync
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static async Task SSESendCloseEventAsync(this HttpContext ctx)
        {
            await ctx.Response.WriteAsync("event: close\n");
            await ctx.Response.WriteAsync("retry: 3\n");
            await ctx.Response.WriteAsync("data: bye\n");
            await ctx.Response.WriteAsync("\n");
            await ctx.Response.Body.FlushAsync();
        }


        // 接收浏览器请求，建立ServerSentEvents通道
        public static HttpContent BuildingSse(this HttpContext ctx)
        {
            var res = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteToStream, new MediaTypeHeaderValue("text/event-stream"));
            return res;
        }

        /// <summary>
        /// WriteToStream
        /// </summary>
        /// <param name="outputStream"></param>
        /// <param name="content"></param>
        /// <param name="context"></param>
        public static void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            StreamWriter streamwriter = new StreamWriter(outputStream);
            _streammessage.TryAdd(streamwriter, streamwriter);
        }

        /// <summary>
        /// SendSseMsg
        /// </summary>
        /// <param name="sseMsg"></param>
        public static void SendSseMsg(SseMessageObject sseMsg)
        {
            MessageCallback(sseMsg);
        }
    }
}
