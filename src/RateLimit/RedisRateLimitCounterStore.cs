using AspNetCoreRateLimit;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace RateLimit
{
    public class RedisRateLimitCounterStore : IRateLimitCounterStore
    {
        private readonly ILogger _logger;
        private readonly IRateLimitCounterStore _memoryCacheStore;
        protected readonly IRedisOperationRepository _redisCache;

        public RedisRateLimitCounterStore(
            IRedisOperationRepository redisCache,
            IMemoryCache memoryCache,
            ILogger<RedisRateLimitCounterStore> logger)
        {
            _logger = logger;
            _memoryCacheStore = new MemoryCacheRateLimitCounterStore(memoryCache);

            _redisCache = redisCache;
        }

        public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await TryRedisCommandAsync(
                () =>
                {
                    return _redisCache.Exist(id);
                },
                () =>
                {
                    return _memoryCacheStore.ExistsAsync(id, cancellationToken);
                });
        }

        public async Task<RateLimitCounter?> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await TryRedisCommandAsync(
                async () =>
                {
                    var value = await _redisCache.Get(id);

                    if (!string.IsNullOrEmpty(value))
                    {
                        return JsonConvert.DeserializeObject<RateLimitCounter?>(value);
                    }

                    return null;
                },
                () =>
                {
                    return _memoryCacheStore.GetAsync(id, cancellationToken);
                });
        }

        public async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _ = await TryRedisCommandAsync(
                async () =>
                {
                    await _redisCache.Remove(id);

                    return true;
                },
                async () =>
                {
                    await _memoryCacheStore.RemoveAsync(id, cancellationToken);

                    return true;
                });
        }

        public async Task SetAsync(string id, RateLimitCounter? entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            _ = await TryRedisCommandAsync(
                async () =>
                {
                    var exprie = expirationTime.HasValue ? Convert.ToInt32(expirationTime.Value.TotalSeconds) : -1;
                    await _redisCache.Set(id, JsonConvert.SerializeObject(entry.Value), TimeSpan.FromHours(1));

                    return true;
                },
                async () =>
                {
                    await _memoryCacheStore.SetAsync(id, entry, expirationTime, cancellationToken);

                    return true;
                });
        }

        private async Task<T> TryRedisCommandAsync<T>(Func<Task<T>> command, Func<Task<T>> fallbackCommand)
        {
            if (_redisCache != null)
            {
                try
                {
                    return await command();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Redis command failed: {ex}");
                }
            }

            return await fallbackCommand();
        }
    }
}
