using AspNetCoreRateLimit;

namespace RateLimit.Models
{
    public class RedisConfig
    {
        public IpRateLimitOptions IpRateLimiting { get; set; }
    }
}
