using StackExchange.Redis;

namespace RateLimit
{
    public static class SevicesExtension
    {
        /// <summary>
        /// AddRedisSetup
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddRedisSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // 配置启动Redis服务，虽然可能影响项目启动速度，但是不能在运行的时候报错，所以是合理的
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                //获取连接字符串
                string redisConfiguration = "114.55.177.197,connectTimeout=1000,connectRetry=1,syncTimeout=10000,DefaultDatabase=8";

                var configuration = ConfigurationOptions.Parse(redisConfiguration, true);

                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddTransient<IRedisOperationRepository, RedisOperationRepository>();

            return services;
        }
    }
}
