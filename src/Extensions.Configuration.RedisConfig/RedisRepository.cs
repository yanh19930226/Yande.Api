using Extensions.Configuration.Redis;
using Extensions.Configuration.Redis.Parsers;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace Extensions.Configuration.RedisConfig
{
    public class RedisRepository : IConfigrationRepository
    {
        private IDatabase _db;
        private int _lastVersion = 1;
        private readonly object _lastVersionLock = new object();
        private readonly RedisOptions _redisOptions;
        public IConfigurationParser Parser { get; set; }

        public RedisRepository(RedisOptions redisOptions)
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisOptions.ConnectionString);
            _redisOptions = redisOptions;
            _db = connection.GetDatabase();
            Parser = new Redis.Json.JsonConfigurationFileParser();
        }

        public IDictionary<string, string> GetConfig()
        {
            var dict = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            //foreach (var prefixKey in _redisOptions.PrefixKeys)
            //{
            //    var fullPrefixKey = $"{_redisOptions.Env}{prefixKey}";

            //    IDictionary<string, string> parsedData = Parser.Parse(GetConfigValue().Result.Value);
            //    var Data = new Dictionary<string, string>(parsedData, StringComparer.OrdinalIgnoreCase);

            //    foreach (var item in Data)
            //    {
            //        var key = item.Key;
            //        var val = item.Value;

            //        if (_redisOptions.KeyMode == RedisConfigrationKeyMode.Json)
            //        {
            //            key = $"{prefixKey}:{key.Replace(fullPrefixKey, string.Empty).Replace("/", ":")}";
            //        }
            //        else if (_redisOptions.KeyMode == RedisConfigrationKeyMode.RemovePrefix)
            //        {
            //            key = key.Replace(fullPrefixKey, string.Empty);
            //        }
            //        else
            //        {
            //            key = key.Replace(_redisOptions.Env, string.Empty);
            //        }

            //        if (dict.ContainsKey(key))
            //        {
            //            dict[key] = val;
            //        }
            //        else
            //        {
            //            dict.TryAdd(key, val);
            //        }
            //    }
            //}
          
            IDictionary<string, string> parsedData = Parser.Parse(GetConfigValue().Result.Value);
            var Data = new Dictionary<string, string>(parsedData, StringComparer.OrdinalIgnoreCase);

            foreach (var item in Data)
            {
                var key = item.Key;
                var val = item.Value;

                //if (_redisOptions.KeyMode == RedisConfigrationKeyMode.Json)
                //{
                //    key = $"{prefixKey}:{key.Replace(fullPrefixKey, string.Empty).Replace("/", ":")}";
                //}
                //else if (_redisOptions.KeyMode == RedisConfigrationKeyMode.RemovePrefix)
                //{
                //    key = key.Replace(fullPrefixKey, string.Empty);
                //}
                //else
                //{
                    //key = key.Replace(_redisOptions.Env, string.Empty);
                //}

                if (dict.ContainsKey(key))
                {
                    dict[key] = val;
                }
                else
                {
                    dict.TryAdd(key, val);
                }
            }

            return dict;
        }
        private async Task<ConfigQueryResult> GetConfigValue() => await Task.Run(() =>
        {
            var result = new ConfigQueryResult();

            if (!_db.KeyExists(_redisOptions.Key))
            {
                result.Exists = false;
                return result;
            }
            result.Exists = true;
            result.Value = _db.StringGet(_redisOptions.Key).ToString();
            result.Version = result.Value.GetHashCode();

            return result;
        });

        private async Task<bool> HasValueChanged()
        {
            ConfigQueryResult queryResult;
            queryResult = await GetConfigValue();
            return queryResult != null && UpdateLastVersion(queryResult);
        }

        private bool UpdateLastVersion(ConfigQueryResult queryResult)
        {
            lock (_lastVersionLock)
            {
                if (queryResult.Version != _lastVersion)
                {
                    _lastVersion = queryResult.Version;

                    return true;
                }
            }

            return false;
        }

        public void Watch(IConfigrationWatcher watcher)
        {
            Task.Run(() =>
            {
                var keys = _redisOptions.PrefixKeys;

                if (!string.IsNullOrEmpty(_redisOptions.Env))
                {
                    keys = _redisOptions.PrefixKeys.Select(prefixKey => $"{ _redisOptions.Env }{prefixKey}").ToList();
                }

                try
                {
                    if (HasValueChanged().Result)
                    {
                        watcher.FireChange();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message + Environment.NewLine + exception.StackTrace);
                }
            });
        }

        #region Dispose

        bool _disposed;
        protected void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            //if (disposing)
            //{
            //    _db.Dispose();
            //}

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        RedisRepository()
        {
            Dispose(false);
        }

        #endregion
    }
}
