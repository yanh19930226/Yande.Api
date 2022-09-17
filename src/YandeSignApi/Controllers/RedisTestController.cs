using InitQ.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisPublishAndSubHelper;
using System;
using System.Threading.Tasks;
using YandeSignApi.Applications.Redis;
using YandeSignApi.Applications.RedisMq;
using YandeSignApi.Models.Dtos;

namespace YandeSignApi.Controllers
{
    /// <summary>
    /// Redis消息队列
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RedisTestController : Controller
    {
        private readonly IRedisOperationRepository _redisOperationRepository;
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
        [Subscribe(RedisMessageQueueKey.TestSubscribeQueue)]
        public async Task SetBusinessSettlerEvent(string msg)
        {
            var response = Response;

            HttpContext.Response.Headers.Add("Content-Type", "text/event-stream");
            await HttpContext.Response.WriteAsync($"id: {Guid.NewGuid().ToString()}\n");
            await HttpContext.Response.WriteAsync($"event:SetBusinessSettlerEvent\n");
            await HttpContext.Response.WriteAsync($"retry: 3\n");
            await HttpContext.Response.WriteAsync($"data: {111}\r\r");

            await HttpContext.Response.Body.FlushAsync();

            Response.Body.Close();
        }

        [HttpGet("EnqueueMsg")]
        public async Task<CallBackResult<string>> EnqueueMsgAsync(string rediskey, string redisValue)
        {
            var jm = new CallBackResult<string>();

            try
            {
                long enqueueLong = default;
                for (int i = 0; i < 1000; i++)
                {
                    enqueueLong = await MyRedisSubPublishHelper.EnqueueListLeftPushAsync(rediskey, redisValue + i);
                }
                jm.Success($"入队的数据长度:{enqueueLong}", "入队成功");
            }
            catch (Exception ex)
            {
                jm.Failed($"出队异常，原因：{ex.Message}");
            }

            return jm;
        }

        [HttpGet("DequeueMsg")]
        public async Task<CallBackResult<string>> DequeueMsgAsync(string rediskey)
        {

            var jm = new CallBackResult<string>();

            try
            {
                string dequeueMsg = await MyRedisSubPublishHelper.DequeueListPopRightAsync(rediskey);
                jm.Success($"出队的数据是:{dequeueMsg}", "出队成功");
            }
            catch (Exception ex)
            {
                jm.Failed($"出队异常，原因：{ex.Message}");
            }
            return jm;
        }
    }
}
