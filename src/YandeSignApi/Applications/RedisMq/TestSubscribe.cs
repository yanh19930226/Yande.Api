using InitQ.Abstractions;
using InitQ.Attributes;
using System.Threading.Tasks;

namespace YandeSignApi.Applications.RedisMq
{
    /// <summary>
    /// TestSubscribe
    /// </summary>
    public class TestSubscribe : IRedisSubscribe
    {
        [Subscribe(RedisMessageQueueKey.TestSubscribeQueue)]
        private async Task TestSubscribeQueue(string msg)
        {
            await Task.CompletedTask;
        }
    }
}
