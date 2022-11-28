using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System.Text;

namespace RateLimit.RedisConfiguration
{
    public class RedisConfigurationProvider : ConfigurationProvider
    {
        private RedisConfigurationSource _source;
        private readonly Task _configurationListeningTask;
        private static readonly object Locker = new object();
        private ConnectionMultiplexer _redisMultiplexer;
        private IDatabase _db = null;

        public RedisConfigurationProvider(
            RedisConfigurationSource source
            )
        {
            _source = source;
            InitConnect(_source.RedisString);
            _configurationListeningTask = new Task(ListenToConfigurationChanges);
        }

        public override void Load()
        {
            LoadAsync().GetAwaiter();
        }

        public async Task LoadAsync()
        {
            Data = await QueryDataAsync();

            //启动配置更改监听
            if (_configurationListeningTask.Status == TaskStatus.Created)
                _configurationListeningTask.Start();
        }

        private async void ListenToConfigurationChanges()
        {
            if (_source.ReloadTime <= 0)
                return;

            while (true)
            {
                Console.WriteLine("监听!!!");
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(_source.ReloadTime));
                    Data = await QueryDataAsync();
                    OnReload();
                }
                catch
                {

                }
            }
        }

        public async Task<IDictionary<string, string>> QueryDataAsync()
        {
            var redisVal = await _db.StringGetAsync(_source.ConfigKey);

            if (redisVal.HasValue)
            {
                var tokens = JToken.Parse(redisVal);

                var res = tokens
                    .Select(k => KeyValuePair.Create
                    (
                        k.Value<string>("Key"),
                        k.Value<string>("Value") != null ? JToken.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(k.Value<string>("Value")))) : null
                    ))
                    .Where(v => !string.IsNullOrWhiteSpace(v.Key))
                    .SelectMany(Flatten)
                    .ToDictionary(v => ConfigurationPath.Combine(v.Key.Split('/')), v => v.Value, StringComparer.OrdinalIgnoreCase);

                return res;
            }
            else { 
            
              return new Dictionary<string, string>();
            
            }
        }

        #region 帮助方法

        private void InitConnect(string redisConnection)
        {
            try
            {
                _redisMultiplexer = ConnectionMultiplexer.Connect(redisConnection);
                _db = _redisMultiplexer.GetDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _redisMultiplexer = null;
                _db = null;
            }
        }


        //递归展平JSON节点
        private static IEnumerable<KeyValuePair<string, string>> Flatten(KeyValuePair<string, JToken> tuple)
        {
            if (!(tuple.Value is JObject value))
                yield break;

            foreach (var property in value)
            {
                var propertyKey = $"{tuple.Key}/{property.Key}";
                switch (property.Value.Type)
                {
                    case JTokenType.Object:
                        string v = Newtonsoft.Json.JsonConvert.SerializeObject(property.Value);
                        yield return KeyValuePair.Create(propertyKey, v);
                        break;
                    case JTokenType.Array:
                        break;
                    default:
                        yield return KeyValuePair.Create(propertyKey, property.Value.Value<string>());
                        break;
                }

            }
        }
        #endregion
    }
}
