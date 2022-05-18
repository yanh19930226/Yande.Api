using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yande.Api.Models.Configs;

namespace Yande.Api
{
    /// <summary>
    /// SevicesExtension
    /// </summary>
    public static class SevicesExtension
    {
        /// <summary>
        /// 注册健康检查组件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration Configuration)
        {
            var mysqlConfig = Configuration.GetMysqlSection().Get<MysqlConfig>();
            var mongoConfig = Configuration.GetMongoDbSection().Get<MongoConfig>();
            var redisConfig = Configuration.GetRedisSection().Get<RedisConfig>();
            services.AddHealthChecks()
                         .AddMySql(mysqlConfig.ConnectionString)
                         .AddMongoDb(mongoConfig.ConnectionString)
                         //.AddRabbitMQ(x =>
                         //{
                         //    return
                         //    Adnc.Infra.EventBus.RabbitMq.RabbitMqConnection.GetInstance(x.GetService<IOptionsMonitor<RabbitMqConfig>>()
                         //        , x.GetService<ILogger<dynamic>>()
                         //    ).Connection;
                         //})
                        .AddRedis(redisConfig.dbconfig.ConnectionString);
            return services;
        }
    }
}
