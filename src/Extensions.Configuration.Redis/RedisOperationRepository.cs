using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RateLimit
{
    public class RedisOperationRepository : IRedisOperationRepository
    {
        private readonly ILogger<RedisOperationRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisOperationRepository(ILogger<RedisOperationRepository> logger, ConnectionMultiplexer redis)
        {
            _logger = logger;
            _redis = redis;
            _database = redis.GetDatabase();
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }

        public async Task Clear()
        {
            foreach (var endPoint in _redis.GetEndPoints())
            {
                var server = GetServer();
                foreach (var key in server.Keys())
                {
                    await _database.KeyDeleteAsync(key);
                }
            }
        }

        public async Task<bool> Exist(string key)
        {
            return await _database.KeyExistsAsync(key);
        }
        public async Task<bool> KeyExpireAsync(string key, DateTime exp)
        {
            return await _database.KeyExpireAsync(key, exp);
        }
        public async Task<string> Get(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task Remove(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                //序列化，将object值生成RedisValue
                await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), cacheTime);
            }
        }

        public async Task<TEntity> Get<TEntity>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.HasValue)
            {
                //需要用的反序列化，将Redis存储的Byte[]，进行反序列化
                return JsonConvert.DeserializeObject<TEntity>(value);
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// 根据key获取RedisValue
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> ListRangeAsync(string redisKey)
        {
            return await _database.ListRangeAsync(redisKey);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync(string redisKey, string redisValue)
        {
            return await _database.ListLeftPushAsync(redisKey, redisValue);
        }
        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, string redisValue)
        {
            return await _database.ListRightPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表尾部插入数组集合。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue)
        {
            var redislist = new List<RedisValue>();
            foreach (var item in redisValue)
            {
                redislist.Add(item);
            }
            return await _database.ListRightPushAsync(redisKey, redislist.ToArray());
        }


        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素  反序列化
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string redisKey) where T : class
        {
            var cacheValue = await _database.ListLeftPopAsync(redisKey);
            if (string.IsNullOrEmpty(cacheValue)) return null;
            var res = JsonConvert.DeserializeObject<T>(cacheValue);
            return res;
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素   反序列化
        /// 只能是对象集合
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string redisKey) where T : class
        {
            var cacheValue = await _database.ListRightPopAsync(redisKey);
            if (string.IsNullOrEmpty(cacheValue)) return null;
            var res = JsonConvert.DeserializeObject<T>(cacheValue);
            return res;
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素   
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListLeftPopAsync(string redisKey)
        {
            return await _database.ListLeftPopAsync(redisKey);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素   
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListRightPopAsync(string redisKey)
        {
            return await _database.ListRightPopAsync(redisKey);
        }

        /// <summary>
        /// 列表长度
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string redisKey)
        {
            return await _database.ListLengthAsync(redisKey);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, int db = -1)
        {
            var result = await _database.ListRangeAsync(redisKey);
            return result.Select(o => o.ToString());
        }

        /// <summary>
        /// 根据索引获取指定位置数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, int start, int stop)
        {
            var result = await _database.ListRangeAsync(redisKey, start, stop);
            return result.Select(o => o.ToString());
        }

        /// <summary>
        /// 删除List中的元素 并返回删除的个数
        /// </summary>
        /// <param name="redisKey">key</param>
        /// <param name="redisValue">元素</param>
        /// <param name="type">大于零 : 从表头开始向表尾搜索，小于零 : 从表尾开始向表头搜索，等于零：移除表中所有与 VALUE 相等的值</param>
        /// <returns></returns>
        public async Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0)
        {
            return await _database.ListRemoveAsync(redisKey, redisValue, type);
        }

        /// <summary>
        /// 清空List
        /// </summary>
        /// <param name="redisKey"></param>
        public async Task ListClearAsync(string redisKey)
        {
            await _database.ListTrimAsync(redisKey, 1, 0);
        }


        /// <summary>
        /// SortedSetAddAsync
        /// </summary>
        /// <param name="redisKey"></param>
        public async Task SortedSetAddAsync(string redisKey, string redisValue, DateTime cacheTime)
        {
            var score = (cacheTime.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            await _database.SortedSetAddAsync(redisKey, redisValue, score);
        }

        #region Hash操作
        /// <summary>
        /// HSET
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HSET(string hkey, string key, string value)
        {
            return await _database.HashSetAsync(hkey, key, value);
        }

        /// <summary>
        /// HSETNX 不存在才设置
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HSETNX(string hkey, string key, string value)
        {
            return await _database.HashSetAsync(hkey, key, value, When.NotExists);
        }

        /// <summary>
        /// HGET 获取hash中的一个key
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<RedisValue> HGET(string hkey, string key)
        {
            return await _database.HashGetAsync(hkey, key);
        }

        /// <summary>
        /// HIncr
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<long> HIncr(string hkey, string key, int count)
        {
            return await _database.HashIncrementAsync(hkey, key, count);
        }

        /// <summary>
        /// hlen 返回hash存在的key个数
        /// </summary>
        /// <param name="hkey"></param>
        /// <returns></returns>
        public async Task<long> HLen(string hkey)
        {
            return await _database.HashLengthAsync(hkey);
        }

        /// <summary>
        /// hdel 删除一个hash里面的key
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> Hdel(string hkey, string key)
        {
            return await _database.HashDeleteAsync(hkey, key);
        }

        /// <summary>
        /// hstrlen hash中key的数据长度
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<long> HashStrLen(string hkey, string key)
        {
            return await _database.HashStringLengthAsync(hkey, key);
        }

        /// <summary>
        /// hexists 判断一个key是否在hash中
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> HashKeyExist(string hkey, string key)
        {
            return await _database.HashExistsAsync(hkey, key);
        }

        /// <summary>
        /// hmset 批量设置多个数据
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task HMSET(string hkey, HashEntry[] value)
        {
            await _database.HashSetAsync(hkey, value);
        }

        /// <summary>
        /// hmget 批量获取多个数据
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> HMGET(string hkey, RedisValue[] hashField)
        {
            return await _database.HashGetAsync(hkey, hashField);
        }

        /// <summary>
        /// hkeys 获取所有的key
        /// </summary>
        /// <param name="hkey"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> HKeys(string hkey)
        {
            return await _database.HashKeysAsync(hkey);
        }

        /// <summary>
        /// hvals 获取所有的value
        /// </summary>
        /// <param name="hkey"></param>
        /// <returns></returns>
        public async Task<RedisValue[]> HVals(string hkey)
        {
            return await _database.HashValuesAsync(hkey);
        }

        /// <summary>
        /// hall 获取所有的键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hkey"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, T>> HashGetAll<T>(string hkey)
        {
            HashEntry[] hashEntries = await _database.HashGetAllAsync(hkey);

            return hashEntries.ToDictionary(
                            x => x.Name.ToString(),
                            x => JsonConvert.DeserializeObject<T>(x.Value),
                            StringComparer.Ordinal);
        }

        /// <summary>
        /// hall 获取所有的键值对
        /// </summary>
        /// <param name="hkey"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> HashAll(string hkey)
        {
            HashEntry[] hashEntries = await _database.HashGetAllAsync(hkey);

            return hashEntries.ToDictionary(
                            x => x.Name.ToString(),
                            x => x.Value.ToString(),
                            StringComparer.Ordinal);

        }
        #endregion

    }
}
