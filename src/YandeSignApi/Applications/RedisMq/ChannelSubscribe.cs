using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;
using YandeSignApi.Applications.Redis;

namespace YandeSignApi.Applications.RedisMq
{
    /// <summary>
    /// ChannelSubscribeA
    /// </summary>
    public class ChannelSubscribeA : IHostedService, IDisposable
    {
        private readonly ILogger<ChannelSubscribeA> _logger;
        private readonly IRedisOperationRepository _redisOperationRepository;

        /// <summary>
        /// ChannelSubscribe
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="redisOperationRepository"></param>
        public ChannelSubscribeA(
            ILogger<ChannelSubscribeA> logger,
            IRedisOperationRepository redisOperationRepository
            )
        {
            _logger = logger;
            _redisOperationRepository = redisOperationRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("程序启动");
            Task.Run(async () =>
            {
                await _redisOperationRepository.SubscribeAsync(RedisMessageQueueKey.TestChannelSubscribeQueue, new Action<RedisChannel, RedisValue>((channel, message) =>
                {
                    Console.WriteLine(RedisMessageQueueKey.TestChannelSubscribeQueue + " 订阅服务A收到消息：" + message);
                }));
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("结束");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("退出");
        }
    }

    /// <summary>
    /// ChannelSubscribeB
    /// </summary>
    public class ChannelSubscribeB : IHostedService, IDisposable
    {
        private readonly ILogger<ChannelSubscribeB> _logger;
        private readonly IRedisOperationRepository _redisOperationRepository;

        /// <summary>
        /// ChannelSubscribe
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="redisOperationRepository"></param>
        public ChannelSubscribeB(
            ILogger<ChannelSubscribeB> logger,
            IRedisOperationRepository redisOperationRepository
            )
        {
            _logger = logger;
            _redisOperationRepository = redisOperationRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("程序启动");
            Task.Run(async () =>
            {
                await _redisOperationRepository.SubscribeAsync(RedisMessageQueueKey.TestChannelSubscribeQueue, new Action<RedisChannel, RedisValue>((channel, message) =>
                {
                    Console.WriteLine(RedisMessageQueueKey.TestChannelSubscribeQueue + " 订阅服务B收到消息：" + message);
                }));
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("结束");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("退出");
        }
    }
}
