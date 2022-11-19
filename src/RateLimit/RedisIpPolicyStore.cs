using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace RateLimit
{
    public class RedisIpPolicyStore : IIpPolicyStore
    {
        private readonly IpRateLimitOptions _options;
        private readonly IpRateLimitPolicies _policies;
        protected readonly IRedisOperationRepository _redisCache;
        public RedisIpPolicyStore(
            IRedisOperationRepository redisCache,
            IOptions<IpRateLimitOptions> options = null,
            IOptions<IpRateLimitPolicies> policies = null)
        {
            _options = options?.Value;
            _policies = policies?.Value;
            _redisCache = redisCache;
        }

        public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _redisCache.Exist($"{_options.IpPolicyPrefix}_{id}");
        }

        public async Task<IpRateLimitPolicies> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            string stored = await _redisCache.Get($"{_options.IpPolicyPrefix}_{id}");

            if (!string.IsNullOrEmpty(stored))
            {
                return JsonConvert.DeserializeObject<IpRateLimitPolicies>(stored);
            }

            return default;
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
        {
            await _redisCache.Remove($"{_options.IpPolicyPrefix}_{id}");
        }

        //public async Task SeedAsync()
        //{
        //    if (_options != null && _policies != null)
        //    {
        //        await _redisCache.Set($"{_options.IpPolicyPrefix}", JsonConvert.SerializeObject(_policies)).ConfigureAwait(false);
        //    }
        //}

        public async Task SetAsync(string id, IpRateLimitPolicies entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
        {
            var exprie = expirationTime.HasValue ? Convert.ToInt32(expirationTime.Value.TotalSeconds) : -1;
            await _redisCache.Set($"{_options.IpPolicyPrefix}_{id}", JsonConvert.SerializeObject(_policies),TimeSpan.FromMinutes(5));
        }

        public Task SeedAsync()
        {
            throw new NotImplementedException();
        }
    }

}
