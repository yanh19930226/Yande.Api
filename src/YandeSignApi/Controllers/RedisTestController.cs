using Microsoft.AspNetCore.Mvc;
using RedisPublishAndSubHelper;
using System;
using System.Threading.Tasks;
using YandeSignApi.Models.Dtos;

namespace YandeSignApi.Controllers
{
    /// <summary>
    /// RedisTestController
    /// </summary>
    public class RedisTestController : Controller
    {
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
