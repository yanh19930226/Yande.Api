using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yande.Core.Redis
{
    public interface IRedisManage
    {
        /// <summary>
        /// 设置一个 键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ts"></param>
        void Set(string key, object value, TimeSpan ts);
        /// <summary>
        ///  //获取 Reids 缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);
        /// <summary>
        /// 获取序列化值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity Get<TEntity>(string key);
        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Get(string key);
        /// <summary>
        /// 移除某个Key和值
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
        /// <summary>
        /// 清空Redis
        /// </summary>
        void Clear();
        /// <summary>
        /// 异步获取 Reids 缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetValueAsync(string key);
        /// <summary>
        /// 异步获取序列化值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TEntity>(string key);
        Task SetAsync(string key, object value, TimeSpan cacheTime);
        Task<bool> GetAsync(string key);
        /// <summary>
        /// 异步移除指定的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveAsync(string key);
        /// <summary>
        /// 异步移除模糊查询到的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveByKey(string key);
        /// <summary>
        /// 异步全部清空
        /// </summary>
        /// <returns></returns>
        Task ClearAsync();
    }
}
