using StackExchange.Redis;

namespace RateLimit
{
    public interface IRedisOperationRepository
    {
        /// <summary>
        /// 设置Key过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        Task<bool> KeyExpireAsync(string key, DateTime exp);
        //获取 Reids 缓存值
        Task<string> Get(string key);

        //获取值，并序列化
        Task<TEntity> Get<TEntity>(string key);

        //保存
        System.Threading.Tasks.Task Set(string key, object value, TimeSpan cacheTime);

        //判断是否存在
        Task<bool> Exist(string key);

        //移除某一个缓存值
        Task Remove(string key);

        //全部清除
        Task Clear();

        /// <summary>
        /// 根据key获取RedisValue
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<RedisValue[]> ListRangeAsync(string redisKey);

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListLeftPushAsync(string redisKey, string redisValue);

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, string redisValue);

        /// <summary>
        /// 在列表尾部插入数组集合。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        Task<long> ListRightPushAsync(string redisKey, IEnumerable<string> redisValue);

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素  反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListLeftPopAsync<T>(string redisKey) where T : class;

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素   反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<T> ListRightPopAsync<T>(string redisKey) where T : class;

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListLeftPopAsync(string redisKey);

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<string> ListRightPopAsync(string redisKey);

        /// <summary>
        /// 列表长度
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task<long> ListLengthAsync(string redisKey);

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ListRangeAsync(string redisKey, int db = -1);

        /// <summary>
        /// 根据索引获取指定位置数据
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ListRangeAsync(string redisKey, int start, int stop);

        /// <summary>
        /// 删除List中的元素 并返回删除的个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<long> ListDelRangeAsync(string redisKey, string redisValue, long type = 0);

        /// <summary>
        /// 清空List
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        Task ListClearAsync(string redisKey);

        /// <summary>
        /// 延迟消息
        /// </summary>
        /// <param name="redisKey">键</param>
        /// <param name="redisValue">消息</param>
        /// <param name="cacheTime">延迟时间</param>
        /// <returns></returns>
        Task SortedSetAddAsync(string redisKey, string redisValue, DateTime cacheTime);

        #region Hash操作
        /// <summary>
        /// HSET
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> HSET(string hkey, string key, string value);

        /// <summary>
        /// HSETNX 不存在才设置
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> HSETNX(string hkey, string key, string value);

        /// <summary>
        /// HGET 获取hash中的一个key
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<RedisValue> HGET(string hkey, string key);

        /// <summary>
        /// HIncr
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<long> HIncr(string hkey, string key, int count);

        /// <summary>
        /// hlen 返回hash存在的key个数
        /// </summary>
        /// <param name="hkey"></param>
        /// <returns></returns>
        Task<long> HLen(string hkey);

        /// <summary>
        /// hdel 删除一个hash里面的key
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Hdel(string hkey, string key);

        /// <summary>
        /// hstrlen hash中key的数据长度
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> HashStrLen(string hkey, string key);

        /// <summary>
        /// hexists 判断一个key是否在hash中
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> HashKeyExist(string hkey, string key);

        /// <summary>
        /// hmset 批量设置多个数据
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task HMSET(string hkey, HashEntry[] value);

        /// <summary>
        /// hmget 批量获取多个数据
        /// </summary>
        /// <param name="hkey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        Task<RedisValue[]> HMGET(string hkey, RedisValue[] hashField);

        /// <summary>
        /// hkeys 获取所有的key
        /// </summary>
        /// <param name="hkey"></param>
        /// <returns></returns>
        Task<RedisValue[]> HKeys(string hkey);

        /// <summary>
        /// hvals 获取所有的value
        /// </summary>
        /// <param name="hkey"></param>
        /// <returns></returns>
        Task<RedisValue[]> HVals(string hkey);

        /// <summary>
        /// hall 获取所有的键值对
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hkey"></param>
        /// <returns></returns>
        Task<Dictionary<string, T>> HashGetAll<T>(string hkey);

        /// <summary>
        /// hall 获取所有的键值对
        /// </summary>
        /// <param name="hkey"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> HashAll(string hkey);
        #endregion

    }
}
