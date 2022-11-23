namespace RateLimit.RedisConfiguration
{
    public class RedisConfigurationSource : IConfigurationSource
    {
        public string RedisString { get; set; }
        public string ConfigKey { get; set; }
        public int ReloadTime { get; set; }

        public RedisConfigurationSource(string redisString, string configKey, int reloadTime)
        {
            RedisString = redisString;
            ConfigKey = configKey;
            ReloadTime = reloadTime;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new RedisConfigurationProvider(this);
        }
    }
}
