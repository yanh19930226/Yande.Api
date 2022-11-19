using AspNetCoreRateLimit;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace RateLimit
{
    public class RedisClientPolicyStore : IClientPolicyStore
    {
        private readonly ClientRateLimitOptions _options;
        private readonly ClientRateLimitPolicies _policies;
        protected readonly IRedisOperationRepository _redisCache;
        public RedisClientPolicyStore(
            IRedisOperationRepository redisCache,
            IOptions<ClientRateLimitOptions> options = null,
            IOptions<ClientRateLimitPolicies> policies = null)
        {
            _options = options?.Value;
            _policies = policies?.Value;
            _redisCache = redisCache;
        }

        public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _redisCache.Exist($"{_options.ClientPolicyPrefix}_{id}");
        }

        public async Task<ClientRateLimitPolicy> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            string stored = await _redisCache.Get($"{_options.ClientPolicyPrefix}_{id}");
            if (!string.IsNullOrEmpty(stored))
            {
                return JsonConvert.DeserializeObject<ClientRateLimitPolicy>(stored);
            }

            return default;
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
        {
            await _redisCache.Remove($"{_options.ClientPolicyPrefix}_{id}");
        }

        //public async Task SeedAsync()
        //{
        //    // on startup, save the IP rules defined in appsettings
        //    if (_options != null && _policies != null)
        //    {
        //        await _redisCache.SetStringAsync($"{_options.ClientPolicyPrefix}", JsonConvert.SerializeObject(_policies), 0).ConfigureAwait(false);
        //    }
        //}

        public async Task SetAsync(string id, ClientRateLimitPolicy entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
        {
            var exprie = expirationTime.HasValue ? Convert.ToInt32(expirationTime.Value.TotalSeconds) : -1;
            await _redisCache.Set($"{_options.ClientPolicyPrefix}", JsonConvert.SerializeObject(_policies), TimeSpan.FromMinutes(1));
        }

        public Task SeedAsync()
        {
            throw new NotImplementedException();
        }
    }
}
