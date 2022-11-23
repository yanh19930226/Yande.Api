using Microsoft.Extensions.Configuration;

namespace Extensions.Configuration.Redis
{
    public static class RedisConfigurationExtensions
    {
        public static IConfigurationBuilder AddRedis(this IConfigurationBuilder builder, string key, CancellationToken cancellationToken)
        {
            return builder.AddRedis(key, cancellationToken, options => { });
        }

        public static IConfigurationBuilder AddRedis(this IConfigurationBuilder builder, string key, CancellationToken cancellationToken, Action<IRedisConfigurationSource> options)
        {
            var source = new RedisConfigurationSource(key,cancellationToken);
            options(source);

            return builder.Add(source);
        }
    }
}
