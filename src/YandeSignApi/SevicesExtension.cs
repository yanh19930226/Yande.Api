using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;
using System;
using YandeSignApi.Applications.Commons;
using YandeSignApi.Applications.Redis;

namespace YandeSignApi
{
    public static class SevicesExtension
    {

        //public static void AddApiThrottleSetup(this IServiceCollection services)
        //{
        //    services.AddMvc(opts =>
        //    {
        //        //这里添加ApiThrottleActionFilter拦截器

        //    });
        //}

        /// <summary>
        /// AddCustomHealthCheck
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("Jobs.MicrosoftBackgroundService-apiself", () => HealthCheckResult.Healthy());
            hcBuilder.AddSqlServer(
                    configuration["ConnectionString"],
                    name: "Jobs.MicrosoftBackgroundService-sqlserver-check",
                    tags: new string[] { "sqlserverdb" });
            //hcBuilder.AddRabbitMQ(
            //        $"amqp://{configuration["EventBusConnection"]}",
            //        name: "Jobs.MicrosoftBackgroundService-rabbitmqbus-check",
            //        tags: new string[] { "rabbitmqbus" });
            hcBuilder.AddRedis(
                configuration["RedisConnectionString"],
                name: "Jobs.MicrosoftBackgroundService-redis-check",
                tags: new string[] { "redisdb" });
            //hcBuilder.AddUrlGroup(new Uri(configuration["testApiUrl"]), name: "testApiUrl-check", tags: new string[] { "testApiUrl" });

            return services;
        }

        public static void AddRedisSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            //配置文件是否启用Redis
            if (AppSettingsConstVars.RedisConfigEnabled)
            {
                // 配置启动Redis服务，虽然可能影响项目启动速度，但是不能在运行的时候报错，所以是合理的
                services.AddSingleton<ConnectionMultiplexer>(sp =>
                {
                    //获取连接字符串
                    string redisConfiguration = AppSettingsConstVars.RedisConfigConnectionString;

                    var configuration = ConfigurationOptions.Parse(redisConfiguration, true);

                    configuration.ResolveDns = true;

                    return ConnectionMultiplexer.Connect(configuration);
                });

                services.AddTransient<IRedisOperationRepository, RedisOperationRepository>();
            }
        }

        public static IServiceCollection AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddOpenApiDocument(settings =>
            {
                //可以设置从注释文件加载，但是加载的内容可被OpenApiTagAttribute特性覆盖
                settings.UseControllerSummaryAsTagDescription = true;
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "YandeSignApi";
                    document.Info.Description = "YandeSignApi";
                    document.Info.TermsOfService = "None";
                };
            });

            return services;
        }

        public static IApplicationBuilder UseCoreSwagger(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();
            return app;
        }
    }
}
