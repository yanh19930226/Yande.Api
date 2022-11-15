﻿
namespace In66.Caching.StackExchange
{
    /// <summary>
    /// Default redis caching provider.
    /// </summary>
    public partial class DefaultRedisProvider : IRedisProvider
    {
        public async Task<dynamic> ScriptEvaluateAsync(string script, RedisKey[] keys = null, RedisValue[] values = null, CommandFlags flags = CommandFlags.None)
        {
            return await _redisDb.ScriptEvaluateAsync(script, keys, values, flags);
        }

        public async Task<dynamic> ScriptEvaluateAsync(string script, object parameters = null, CommandFlags flags = CommandFlags.None)
        {
            var prepared = LuaScript.Prepare(script);
            var result = await _redisDb.ScriptEvaluateAsync(prepared, parameters, flags);
            return result;
        }
    }
}
