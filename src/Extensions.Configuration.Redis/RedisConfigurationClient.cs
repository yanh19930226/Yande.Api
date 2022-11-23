using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using StackExchange.Redis;

namespace Extensions.Configuration.Redis
{
    internal sealed class RedisConfigurationClient 
    {
        private readonly object _lastVersionLock = new object();
        private int _lastVersion = 1;
        private RedisConfigurationSource _source;
        private string _configValueVersionPostfix = "_Version";

        private ConfigurationReloadToken _reloadToken = new ConfigurationReloadToken();

        public RedisConfigurationClient(RedisConfigurationSource source)
        {
            _source = source;
        }

        public async Task<ConfigQueryResult> GetConfig()
        {
            var result = await GetConfigValue();
            UpdateLastVersion(result);
            return result;
        }

        public IChangeToken Watch(Action<RedisWatchExceptionContext> onException)
        {
            Task.Run(() => PollForChanges(onException));
            return _reloadToken;
        }

        private async Task PollForChanges(Action<RedisWatchExceptionContext> onException)
        {
            while (!_source.CancellationToken.IsCancellationRequested)
            {
                try
                {
                    if (await HasValueChanged())
                    {
                        var previousToken = Interlocked.Exchange(ref _reloadToken, new ConfigurationReloadToken());
                        previousToken.OnReload();
                        return;
                    }
                }
                catch (Exception exception)
                {
                    var exceptionContext = new RedisWatchExceptionContext(_source, exception);
                    onException?.Invoke(exceptionContext);
                }

                await Task.Delay(2000);
            }
        }

        private async Task<bool> HasValueChanged()
        {
            ConfigQueryResult queryResult;
            queryResult = await GetConfigValue();
            //return queryResult != null && UpdateLastVersion(queryResult);
            return queryResult != null;

        }

        private async Task<ConfigQueryResult> GetConfigValue() => await Task.Run(() =>
        {
            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(_source.Server);

            IDatabase db = connection.GetDatabase(8);

            var result = new ConfigQueryResult();

            if (!db.KeyExists(_source.Key))
            {
                result.Exists = false;
                return result;
            }

            result.Exists = true;
            result.Value = db.StringGet(_source.Key).ToString();
            result.Version = Convert.ToInt32(db.StringGet($"{_source.Key}{_configValueVersionPostfix}"));
            return result;
        });

        private bool UpdateLastVersion(ConfigQueryResult queryResult)
        {
            lock (_lastVersionLock)
            {
                if (queryResult.Version > _lastVersion)
                {
                    _lastVersion = queryResult.Version;
                    return true;
                }
            }

            return false;
        }
    }
}
