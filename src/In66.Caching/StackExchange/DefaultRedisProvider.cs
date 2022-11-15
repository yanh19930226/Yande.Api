namespace In66.Caching.StackExchange
{
    /// <summary>
    /// Default redis caching provider.
    /// </summary>
    public partial class DefaultRedisProvider : IRedisProvider
    {
        /// <summary>
        /// The serializer.
        /// </summary>
        public IRedisSerializer Serializer => _serializer;

        /// <summary>
        /// The cache.
        /// </summary>
        private readonly IDatabase _redisDb;

        /// <summary>
        /// The serializer.
        /// </summary>
        private readonly IRedisSerializer _serializer;

        /// <summary>
        /// The servers.
        /// </summary>
        private readonly IEnumerable<IServer> _servers;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;


        public string Name => RedisConstValue.Provider.StackExchange;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Adnc.Infra.Caching.Redis.DefaultRedisCachingProvider"/> class.
        /// </summary>
        /// <param name="dbProviders">Db providers.</param>
        /// <param name="serializers">Serializers.</param>
        /// <param name="cacheOptions">CacheOptions.</param>
        /// <param name="loggerFactory">Logger factory.</param>
        public DefaultRedisProvider(
            DefaultDatabaseProvider dbProviders,
            IEnumerable<IRedisSerializer> serializers,
            IOptions<RedisOptions> cacheOptions,
            ILoggerFactory loggerFactory = null)
        {
            ArgumentCheck.NotNull(dbProviders, nameof(dbProviders));

            this._logger = loggerFactory?.CreateLogger<CachingProvider>();
            this._redisDb = dbProviders.GetDatabase();
            this._servers = dbProviders.GetServerList();
            this._serializer = !string.IsNullOrWhiteSpace(cacheOptions.Value.SerializerName)
                           ? serializers.Single(x => x.Name.Equals(cacheOptions.Value.SerializerName))
                           : serializers.Single(x => x.Name.Equals(RedisConstValue.Serializer.DefaultProtobufSerializerName));
        }
    }
}
