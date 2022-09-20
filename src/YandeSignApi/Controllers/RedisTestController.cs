using InitQ.Abstractions;
using InitQ.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedisPublishAndSubHelper;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using YandeSignApi.Applications.Extensions;
using YandeSignApi.Applications.Redis;
using YandeSignApi.Applications.RedisMq;
using YandeSignApi.Models.Dtos;
using static YandeSignApi.Applications.Extensions.SSEHttpContextExtension;

namespace YandeSignApi.Controllers
{
    /// <summary>
    /// Redis消息队列
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RedisTestController : Controller, IRedisSubscribe
    {
        private readonly IRedisOperationRepository _redisOperationRepository;  

        private static readonly ConcurrentDictionary<StreamWriter, StreamWriter> _streammessage = new ConcurrentDictionary<StreamWriter, StreamWriter>();

        private static void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            StreamWriter streamwriter = new StreamWriter(outputStream);
            _streammessage.TryAdd(streamwriter, streamwriter);
        }

        public static void SendSseMsg(SseMessageObject sseMsg)
        {
            MessageCallback(sseMsg);
        }

        private static void MessageCallback(SseMessageObject sseMsg)
        {
            foreach (var subscriber in _streammessage.ToArray())
            {
                try
                {
                    subscriber.Value.WriteLine(string.Format("id: {0}\n", sseMsg.MsgId));
                    subscriber.Value.WriteLine(string.Format("event: {0}\n", "SetBusinessSettlerEvent"));
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

        private HttpContext _ctx;
        /// <summary>
        /// RedisTestController
        /// </summary>
        /// <param name="redisOperationRepository"></param>
        public RedisTestController(
            IRedisOperationRepository redisOperationRepository
            )
        {
            _redisOperationRepository = redisOperationRepository;
        }

        /// <summary>
		/// RedisMq发布订阅
		/// </summary>
		/// <returns></returns>
		[HttpPost]
        public async Task RedisMq()
        {
            await _redisOperationRepository.ListRightPushAsync(RedisMessageQueueKey.TestSubscribeQueue, "test");
        }

        /// <summary>
        /// RedisMq延迟
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task RedisMqDelay()
        {
            var dtNow = DateTime.Now;

            await _redisOperationRepository.SortedSetAddAsync(RedisMessageQueueKey.TestSubscribeDelayQueue, $"{dtNow.ToString("yyyy-MM-dd HH:mm:ss")}", dtNow.AddSeconds(10));
        }

        /// <summary>
        /// RedisMq发布广播消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task RedisMqChannel()
        {
            await _redisOperationRepository.PublishAsync(RedisMessageQueueKey.TestChannelSubscribeQueue, "TestChannelSubscribeQueue");
        }

        [HttpGet]
        public HttpResponseMessage SetBusinessSettlerEvent(HttpRequestMessage request)
        {
            HttpResponseMessage response = request.CreateResponse();
            response.Content = new System.Net.Http.PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteToStream, new MediaTypeHeaderValue("text/event-stream"));
            return response;
        }

        public class MyPushStreamResult : ActionResult
        {
            public string ContentType { get; private set; }
            public PushStreamContent Stream { get; private set; }
            public MyPushStreamResult(PushStreamContent stream, string contentType)
            {
                Stream = stream;
                ContentType = contentType;
            }
            public override async Task ExecuteResultAsync(ActionContext context)
            {
                var response = context.HttpContext.Response;
                response.ContentType = ContentType;
                await Stream.CopyToAsync(response.Body);
            }
        }


        /// <summary>
        /// TestMake
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<CallBackResult> TestMake()
        {
            CallBackResult jm = new CallBackResult();
            var SysUserId = "84FO0SMD00J8D1ZF";
            await _redisOperationRepository.HSETNX(SysUserId.ToString(), SysUserId, SysUserId);

            await _redisOperationRepository.ListRightPushAsync(RedisMessageQueueKey.TestSubscribeQueue, SysUserId.ToString());

            return jm;
        }


        [HttpGet]
        [Subscribe(RedisMessageQueueKey.TestSubscribeQueue)]
        public async Task TriggerEvent()
        {
            var sseMsg = new SseMessageObject();
            sseMsg.MsgId = "1101";
            sseMsg.MsgData = "自定义告警消息";
            SendSseMsg(sseMsg);
        }
    }
}
