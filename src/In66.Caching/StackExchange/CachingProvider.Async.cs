namespace In66.Caching.StackExchange
{
    /// <summary>
    /// Default redis caching provider.
    /// </summary>
    public partial class CachingProvider : AbstracCacheProvider, ICacheProvider
    {
        /// <summary>
        /// Gets the specified cacheKey async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cacheKey">Cache key.</param>
        /// <param name="type">Object Type.</param>
        protected override async Task<object> BaseGetAsync(string cacheKey, Type type)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            if (!_RedisOptions.Value.PenetrationSetting.Disable && _RedisOptions.Value.EnableBloomFilter)
            {
                var exists = await _redisDb.BfExistsAsync(_RedisOptions.Value.PenetrationSetting.BloomFilterSetting.Name, cacheKey);
                if (!exists)
                {
                    if (_RedisOptions.Value.EnableLogging)
                        _logger?.LogInformation($"Cache Penetrated : cachekey = {cacheKey}");
                    return null;
                }
            }

            var result = await _redisDb.StringGetAsync(cacheKey);
            if (!result.IsNull)
            {
                _cacheStats.OnHit();

                if (_RedisOptions.Value.EnableLogging)
                    _logger?.LogInformation($"Cache Hit : cachekey = {cacheKey}");

                var value = _serializer.Deserialize(result, type);
                return value;
            }
            else
            {
                _cacheStats.OnMiss();

                if (_RedisOptions.Value.EnableLogging)
                    _logger?.LogInformation($"Cache Missed : cachekey = {cacheKey}");

                return null;
            }
        }

        /// <summary>
        /// Gets the specified cacheKey, dataRetriever and expiration async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cacheKey">Cache key.</param>
        /// <param name="dataRetriever">Data retriever.</param>
        /// <param name="expiration">Expiration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected override async Task<ReValue<T>> BaseGetAsync<T>(string cacheKey, Func<Task<T>> dataRetriever, TimeSpan expiration)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));

            if (!_RedisOptions.Value.PenetrationSetting.Disable && _RedisOptions.Value.EnableBloomFilter)
            {
                var exists = await _redisDb.BfExistsAsync(_RedisOptions.Value.PenetrationSetting.BloomFilterSetting.Name, cacheKey);
                if (!exists)
                {
                    if (_RedisOptions.Value.EnableLogging)
                        _logger?.LogInformation($"Cache Penetrated : cachekey = {cacheKey}");
                    return ReValue<T>.NoValue;
                }
            }

            var result = await _redisDb.StringGetAsync(cacheKey);
            if (!result.IsNull)
            {
                _cacheStats.OnHit();

                if (_RedisOptions.Value.EnableLogging)
                    _logger?.LogInformation($"Cache Hit : cachekey = {cacheKey}");

                var value = _serializer.Deserialize<T>(result);
                return new ReValue<T>(value, true);
            }

            _cacheStats.OnMiss();

            if (_RedisOptions.Value.EnableLogging)
                _logger?.LogInformation($"Cache Missed : cachekey = {cacheKey}");

            var flag = await _redisDb.LockAsync(cacheKey, _RedisOptions.Value.LockMs / 1000);

            if (!flag.Success)
            {
                await Task.Delay(_RedisOptions.Value.SleepMs);
                return await GetAsync(cacheKey, dataRetriever, expiration);
            }

            try
            {
                var item = await dataRetriever();
                if (item != null)
                {
                    await SetAsync(cacheKey, item, expiration);
                    return new ReValue<T>(item, true);
                }
                else
                {
                    return ReValue<T>.NoValue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                //remove mutex key
                await _redisDb.SafedUnLockAsync(cacheKey, flag.LockValue);
            }
        }

        /// <summary>
        /// Gets the specified cacheKey async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cacheKey">Cache key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected override async Task<ReValue<T>> BaseGetAsync<T>(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            if (!_RedisOptions.Value.PenetrationSetting.Disable && _RedisOptions.Value.EnableBloomFilter)
            {
                var exists = await _redisDb.BfExistsAsync(_RedisOptions.Value.PenetrationSetting.BloomFilterSetting.Name, cacheKey);
                if (!exists)
                {
                    if (_RedisOptions.Value.EnableLogging)
                        _logger?.LogInformation($"Cache Penetrated : cachekey = {cacheKey}");
                    return ReValue<T>.NoValue;
                }
            }

            var result = await _redisDb.StringGetAsync(cacheKey);
            if (!result.IsNull)
            {
                _cacheStats.OnHit();

                if (_RedisOptions.Value.EnableLogging)
                    _logger?.LogInformation($"Cache Hit : cachekey = {cacheKey}");

                var value = _serializer.Deserialize<T>(result);
                return new ReValue<T>(value, true);
            }
            else
            {
                _cacheStats.OnMiss();

                if (_RedisOptions.Value.EnableLogging)
                    _logger?.LogInformation($"Cache Missed : cachekey = {cacheKey}");

                return ReValue<T>.NoValue;
            }
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <returns>The count.</returns>
        /// <param name="prefix">Prefix.</param>
        protected override Task<int> BaseGetCountAsync(string prefix = "")
        {
            if (string.IsNullOrWhiteSpace(prefix))
            {
                var allCount = 0;

                foreach (var server in _servers)
                    allCount += (int)server.DatabaseSize(_redisDb.Database);

                return Task.FromResult(allCount);
            }

            return Task.FromResult(this.SearchRedisKeys(this.HandlePrefix(prefix)).Length);
        }

        /// <summary>
        /// Removes the specified cacheKey async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cacheKey">Cache key.</param>
        protected override async Task BaseRemoveAsync(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            await _redisDb.KeyDeleteAsync(cacheKey);
        }

        /// <summary>
        /// Sets the specified cacheKey, ReValue and expiration async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cacheKey">Cache key.</param>
        /// <param name="ReValue">Cache value.</param>
        /// <param name="expiration">Expiration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected override async Task BaseSetAsync<T>(string cacheKey, T ReValue, TimeSpan expiration)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            ArgumentCheck.NotNull(ReValue, nameof(ReValue));
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));

            if (_RedisOptions.Value.MaxRdSecond > 0)
            {
                var addSec = new Random().Next(1, _RedisOptions.Value.MaxRdSecond);
                expiration = expiration.Add(TimeSpan.FromSeconds(addSec));
            }

            await _redisDb.StringSetAsync(
                    cacheKey,
                    _serializer.Serialize(ReValue),
                    expiration);
        }

        /// <summary>
        /// Existses the specified cacheKey async.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="cacheKey">Cache key.</param>
        protected override async Task<bool> BaseExistsAsync(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            return await _redisDb.KeyExistsAsync(cacheKey);
        }

        /// <summary>
        /// Removes cached item by cachekey's prefix async.
        /// </summary>
        /// <param name="prefix">Prefix of CacheKey.</param>
        protected override async Task BaseRemoveByPrefixAsync(string prefix)
        {
            ArgumentCheck.NotNullOrWhiteSpace(prefix, nameof(prefix));

            prefix = this.HandlePrefix(prefix);

            if (_RedisOptions.Value.EnableLogging)
                _logger?.LogInformation($"RemoveByPrefixAsync : prefix = {prefix}");

            var redisKeys = this.SearchRedisKeys(prefix);

            await _redisDb.KeyDeleteAsync(redisKeys);
        }

        /// <summary>
        /// Sets all async.
        /// </summary>
        /// <returns>The all async.</returns>
        /// <param name="values">Values.</param>
        /// <param name="expiration">Expiration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected override async Task BaseSetAllAsync<T>(IDictionary<string, T> values, TimeSpan expiration)
        {
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));
            ArgumentCheck.NotNullAndCountGTZero(values, nameof(values));

            var tasks = new List<Task>();

            foreach (var item in values)
                tasks.Add(SetAsync(item.Key, item.Value, expiration));

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Gets all async.
        /// </summary>
        /// <returns>The all async.</returns>
        /// <param name="cacheKeys">Cache keys.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected override async Task<IDictionary<string, ReValue<T>>> BaseGetAllAsync<T>(IEnumerable<string> cacheKeys)
        {
            ArgumentCheck.NotNullAndCountGTZero(cacheKeys, nameof(cacheKeys));

            var keyArray = cacheKeys.ToArray();
            var values = await _redisDb.StringGetAsync(keyArray.Select(k => (RedisKey)k).ToArray());

            var result = new Dictionary<string, ReValue<T>>();
            for (int i = 0; i < keyArray.Length; i++)
            {
                var cachedValue = values[i];
                if (!cachedValue.IsNull)
                    result.Add(keyArray[i], new ReValue<T>(_serializer.Deserialize<T>(cachedValue), true));
                else
                    result.Add(keyArray[i], ReValue<T>.NoValue);
            }

            return result;
        }

        /// <summary>
        /// Gets the by prefix async.
        /// </summary>
        /// <returns>The by prefix async.</returns>
        /// <param name="prefix">Prefix.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected override async Task<IDictionary<string, ReValue<T>>> BaseGetByPrefixAsync<T>(string prefix)
        {
            ArgumentCheck.NotNullOrWhiteSpace(prefix, nameof(prefix));

            prefix = this.HandlePrefix(prefix);

            var redisKeys = this.SearchRedisKeys(prefix);

            var values = (await _redisDb.StringGetAsync(redisKeys)).ToArray();

            var result = new Dictionary<string, ReValue<T>>();
            for (int i = 0; i < redisKeys.Length; i++)
            {
                var cachedValue = values[i];
                if (!cachedValue.IsNull)
                    result.Add(redisKeys[i], new ReValue<T>(_serializer.Deserialize<T>(cachedValue), true));
                else
                    result.Add(redisKeys[i], ReValue<T>.NoValue);
            }

            return result;
        }

        /// <summary>
        /// Removes all async.
        /// </summary>
        /// <returns>The all async.</returns>
        /// <param name="cacheKeys">Cache keys.</param>
        protected override async Task BaseRemoveAllAsync(IEnumerable<string> cacheKeys)
        {
            ArgumentCheck.NotNullAndCountGTZero(cacheKeys, nameof(cacheKeys));

            var redisKeys = cacheKeys.Where(k => !string.IsNullOrEmpty(k)).Select(k => (RedisKey)k).ToArray();
            if (redisKeys.Length > 0)
                await _redisDb.KeyDeleteAsync(redisKeys);
        }

        /// <summary>
        /// Flush All Cached Item async.
        /// </summary>
        /// <returns>The async.</returns>
        protected override async Task BaseFlushAsync()
        {
            if (_RedisOptions.Value.EnableLogging)
                _logger?.LogInformation("Redis -- FlushAsync");

            var tasks = new List<Task>();

            foreach (var server in _servers)
            {
                tasks.Add(server.FlushDatabaseAsync(_redisDb.Database));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Tries the set async.
        /// </summary>
        /// <returns>The set async.</returns>
        /// <param name="cacheKey">Cache key.</param>
        /// <param name="ReValue">Cache value.</param>
        /// <param name="expiration">Expiration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        protected override Task<bool> BaseTrySetAsync<T>(string cacheKey, T ReValue, TimeSpan expiration)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));
            ArgumentCheck.NotNull(ReValue, nameof(ReValue));
            ArgumentCheck.NotNegativeOrZero(expiration, nameof(expiration));

            if (_RedisOptions.Value.MaxRdSecond > 0)
            {
                var addSec = new Random().Next(1, _RedisOptions.Value.MaxRdSecond);
                expiration = expiration.Add(TimeSpan.FromSeconds(addSec));
            }

            return _redisDb.StringSetAsync(
                cacheKey,
                _serializer.Serialize(ReValue),
                expiration,
                When.NotExists,
                CommandFlags.None
                );
        }

        /// <summary>
        /// Get the expiration of cache key
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <returns>expiration</returns>
        protected override async Task<TimeSpan> BaseGetExpirationAsync(string cacheKey)
        {
            ArgumentCheck.NotNullOrWhiteSpace(cacheKey, nameof(cacheKey));

            var timeSpan = await _redisDb.KeyTimeToLiveAsync(cacheKey);
            return timeSpan.HasValue ? timeSpan.Value : TimeSpan.Zero;
        }

        /// <summary>
        /// Get the expiration of cache key
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <returns>expiration</returns>
        protected override async Task BaseKeyExpireAsync(IEnumerable<string> cacheKeys, int seconds)
        {
            ArgumentCheck.NotNullAndCountGTZero(cacheKeys, nameof(cacheKeys));

            await _redisDb.KeyExpireAsync(cacheKeys, seconds);
        }
    }
}
