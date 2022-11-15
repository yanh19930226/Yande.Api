namespace In66.Caching.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInfraCaching(this IServiceCollection services, IConfigurationSection redisSection)
        {
            if (services.HasRegistered(nameof(AddInfraCaching)))
                return services;

            services
                .Configure<RedisOptions>(redisSection)
                .Configure<RedisConfig>(redisSection)
                .AddSingleton<ICachingKeyGenerator, DefaultCachingKeyGenerator>()
                .AddScoped<CachingInterceptor>()
                .AddScoped<CachingAsyncInterceptor>();
            var serviceType = typeof(IRedisSerializer);
            var implementations = serviceType.Assembly.ExportedTypes.Where(type => type.IsAssignableTo(serviceType) && type.IsNotAbstractClass(true)).ToList();
            implementations.ForEach(implementationType => services.AddSingleton(serviceType, implementationType));

            var redisConfig = redisSection.Get<RedisConfig>();
            switch (redisConfig.Provider)
            {
                case RedisConstValue.Provider.StackExchange:
                    AddStackExchange(services);
                    break;
                case RedisConstValue.Provider.ServiceStack:
                    break;
                case RedisConstValue.Provider.FreeRedis:
                    break;
                case RedisConstValue.Provider.CSRedis:
                    break;
                default:
                    throw new NotSupportedException(nameof(redisConfig.Provider));
            }
            return services;
        }

        public static IServiceCollection AddStackExchange(IServiceCollection services)
        {
            return
                services
                .AddSingleton<DefaultDatabaseProvider>()
                .AddSingleton<DefaultRedisProvider>()
                .AddSingleton<IRedisProvider>(x => x.GetRequiredService<DefaultRedisProvider>())
                .AddSingleton<IDistributedLocker>(x => x.GetRequiredService<DefaultRedisProvider>())
                .AddSingleton<ICacheProvider, CachingProvider>();
        }
    }
}
