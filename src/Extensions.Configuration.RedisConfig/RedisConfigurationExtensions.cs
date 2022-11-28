using Extensions.Configuration.RedisConfig;
using Microsoft.Extensions.Configuration;

namespace Extensions.Configuration.Redis
{
    public static class RedisConfigurationExtensions
    {
        public static IConfigurationBuilder AddRedisConfiguration(this IConfigurationBuilder builder, RedisOptions options,  bool reloadOnChange = false, Action < IConfigurationRoot> actionOnChange = null)
        {
            var configRepository = new RedisRepository(options);

            return builder.Add(new RedisConfigurationProvider(configRepository,reloadOnChange,actionOnChange));
        }
    }
}
