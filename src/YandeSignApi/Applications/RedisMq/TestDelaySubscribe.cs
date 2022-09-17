using InitQ.Abstractions;
using InitQ.Attributes;
using System;
using System.Threading.Tasks;

namespace YandeSignApi.Applications.RedisMq
{
    /// <summary>
    /// TestDelaySubscribe
    /// </summary>
    public class TestDelaySubscribe : IRedisSubscribe
    {
        [SubscribeDelay(RedisMessageQueueKey.TestSubscribeDelayQueue)]
        private async Task TestSubscribeDelayQueue(string msg)
        {
            //接收时间
            var recievetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            await Task.CompletedTask;
        }
    }
}
