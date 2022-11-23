namespace RateLimit.RedisConfiguration
{
    public static class RedisExtensions
    {
        public static IConfigurationBuilder AddRedisConfiguration(
            this IConfigurationBuilder builder, string redisString,
            string configKey, int reloadTime = 10)
        {
            return builder.Add(new RedisConfigurationSource(redisString, configKey, reloadTime));
        }
    }
}
