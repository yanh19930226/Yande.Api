using Extensions.Configuration.RedisConfig;
using Microsoft.Extensions.Configuration;

namespace Extensions.Configuration.Redis
{
    internal sealed class RedisConfigurationProvider : ConfigurationProvider, IConfigurationSource, IConfigrationWatcher
    {
        private readonly RedisRepository _redisRepository;
        private readonly Action<IConfigurationRoot> _actionOnChange;

        public RedisConfigurationProvider(RedisRepository redisRepository, bool reloadOnChange, Action<IConfigurationRoot> actionOnChange)
        {
            _redisRepository = redisRepository;
            _actionOnChange = actionOnChange;
        }

        public override void Load()
        {
            Data = _redisRepository.GetConfig();
        }

        private void Reload()
        {
            Load();

            //return the latest configuration
            if (_actionOnChange != null)
            {
                var builder = new ConfigurationBuilder().AddInMemoryCollection(Data).Build();
                _actionOnChange.Invoke(builder);
            }
        }

        public void FireChange()
        {
            Reload();
            OnReload();
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) => this;
    }
}
