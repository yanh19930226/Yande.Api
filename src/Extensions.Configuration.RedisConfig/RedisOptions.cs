namespace Extensions.Configuration.RedisConfig
{
    public class RedisOptions
    {
        /// <summary>
        /// Redis Connection String
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Environment. /dev or /uat or /prod  Default : Empty String
        /// </summary>
        public string Env { get; set; } = string.Empty;
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// config prefixKeys, no need to include env value
        /// </summary>
        public List<string> PrefixKeys { get; set; }
        /// <summary>
        /// String containing username for etcd basic auth. Default : Empty String
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// String containing password for etcd basic auth. Default : Empty String
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Set key mode. Default : Json
        /// </summary>
        public RedisConfigrationKeyMode KeyMode { get; set; } = RedisConfigrationKeyMode.Json;
    }
}
