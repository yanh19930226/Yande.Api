using AspNetCoreRateLimit;
using FileStorage.AliCloud;
using FileStorage.TencentCloud;
using HealthChecks.UI.Client;
using Logger.LocalFile.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShardingCore;
using ShardingCore.Bootstrapers;
using ShardingCore.TableExists;
using SMS.AliCloud;
using SMS.TencentCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YandeSignApi.Applications.Commons;
using YandeSignApi.Applications.Filters;
using YandeSignApi.Applications.Middlewares;
using YandeSignApi.Applications.SecurityAuthorization.RsaChecker;
using YandeSignApi.Data;
using YandeSignApi.Models.ShardingCore;

namespace YandeSignApi
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// </summary>
        public IWebHostEnvironment Env { get; }
        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddHealthChecks()
            //.AddCheck<DatabaseHealthCheck>("sql");
            //services.AddHealthChecksUI();
            services.AddSingleton(new AppSettingsHelper(Env.ContentRootPath));
            services.AddControllers(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
                options.Filters.Add<ValidateModelStateFilter>();
            });

            //services.AddCustomHealthCheck(this.Configuration);
            //services.AddHealthChecksUI(setupSettings =>
            //{
            //    //检测站点，可以添加多条，UI中会把站点内的检测点分组显示（也可通过配置文件实现）
            //    setupSettings.AddHealthCheckEndpoint(name: "localhost-5000", uri: "http://localhost:5000/health");
            //    //当检测出异常结果时发送消息给api
            //    setupSettings.AddWebhookNotification("messageWebhook",
            //        uri: "http://localhost:5008/WeatherForecast/message",
            //        payload: "{ \"message\": \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
            //        restorePayload: "{ \"message\": \"[[LIVENESS]] is back to life\"}");
            //    setupSettings.SetMinimumSecondsBetweenFailureNotifications(60);
            //    setupSettings.SetEvaluationTimeInSeconds(10);
            //}).AddSqlServerStorage(Configuration["HealthStorageConnectionString"]);//数据库持久化

            services.AddRedisSetup();

            #region 后台任务

            services.AddHostedService<LogClearTask>();

            #endregion

            #region 注册短信服务

            //services.AddTencentCloudSMS(options =>
            //{
            //    var settings = hostContext.Configuration.GetSection("TencentCloudSMS").Get<SMS.TencentCloud.Models.SMSSetting>();
            //    options.AppId = settings.AppId;
            //    options.SecretId = settings.SecretId;
            //    options.SecretKey = settings.SecretKey;
            //});

            //services.AddAliCloudSMS(options =>
            //{
            //    var settings = hostContext.Configuration.GetSection("AliCloudSMS").Get<SMS.AliCloud.Models.SMSSetting>();
            //    options.AccessKeyId = settings.AccessKeyId;
            //    options.AccessKeySecret = settings.AccessKeySecret;
            //});

            #endregion

            #region 注册文件服务

            //services.AddTencentCloudStorage(options =>
            //{
            //    var settings = hostContext.Configuration.GetSection("TencentCloudFileStorage").Get<FileStorage.TencentCloud.Models.FileStorageSetting>();
            //    options.AppId = settings.AppId;
            //    options.Region = settings.Region;
            //    options.SecretId = settings.SecretId;
            //    options.SecretKey = settings.SecretKey;
            //    options.BucketName = settings.BucketName;
            //});

            //services.AddAliCloudStorage(options =>
            //{
            //    var settings = hostContext.Configuration.GetSection("AliCloudFileStorage").Get<FileStorage.AliCloud.Models.FileStorageSetting>();
            //    options.Endpoint = settings.Endpoint;
            //    options.AccessKeyId = settings.AccessKeyId;
            //    options.AccessKeySecret = settings.AccessKeySecret;
            //    options.BucketName = settings.BucketName;
            //});

            #endregion

            #region Swagger
            services.AddSwaggerSetup();
            #endregion

            #region Rsa加密
            services.AddAuthentication().AddAuthSecurityRsa();
            //services.AddSingleton(sp =>
            //{
            //    return new RsaOptions()
            //    {
            //        PrivateKey = Configuration.GetSection("RsaConfig")["PrivateKey"],
            //    };
            //});
            #endregion

            #region 限流
            //需要从加载配置文件appsettings.json
            services.AddOptions();
            //需要存储速率限制计算器和ip规则
            services.AddMemoryCache();

            //从appsettings.json中加载常规配置
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //从appsettings.json中加载Ip规则
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            //注入计数器和规则存储
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            //配置（解析器、计数器密钥生成器）
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            #endregion


            ILoggerFactory efLogger = LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
            });

            services.AddShardingDbContext<DefaultDbContext>()
                    .AddEntityConfig(o =>
                    {
                        o.ThrowIfQueryRouteNotMatch = false;
                        o.CreateShardingTableOnStart = true;
                        o.EnsureCreatedWithOutShardingTable = true;
                        o.AddShardingTableRoute<OrderByHourRoute>();
                    })
                    .AddConfig(o =>
                    {
                        o.ConfigId = "c1";
                        o.AddDefaultDataSource("ds0", "server=114.55.177.197;port=3306;database=shardingTest;userid=root;password=66^^66;");
                        o.UseShardingQuery((conn, b) =>
                        {
                            b.UseMySql(conn, new MySqlServerVersion(new Version())).UseLoggerFactory(efLogger);
                        });
                        o.UseShardingTransaction((conn, b) =>
                        {
                            b.UseMySql(conn, new MySqlServerVersion(new Version())).UseLoggerFactory(efLogger);
                        });
                        o.ReplaceTableEnsureManager(sp => new MySqlTableEnsureManager<DefaultDbContext>());
                    }).EnsureConfig();

            
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.GetRequiredService<IShardingBootstrapper>().Start();

            #region 中间件
            // 异常处理中间件
            app.UseExceptionHandlerMidd();
            // 记录请求与返回数据 (注意开启权限，不然本地无法写入)
            app.UseRequestResponseLog();
            // 记录ip请求 (注意开启权限，不然本地无法写入)
            app.UseIpLogMildd();
            #endregion

            //app.UseHealthChecksUI();

            #region Swagger
            app.UseCoreSwagger();
            #endregion

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseIpRateLimiting();

            app.UseClientRateLimiting();

            //app.UseHealthChecksUI();

            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapHealthChecks("/health", new HealthCheckOptions
                //{
                //    ResultStatusCodes = new Dictionary<HealthStatus, int> { { HealthStatus.Unhealthy, 420 }, { HealthStatus.Healthy, 200 }, { HealthStatus.Degraded, 419 } }
                //});
                //endpoints.MapHealthChecks("/health");
                //endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                //{
                //    //Predicate = _ => true,
                //    //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                //});
                //endpoints.MapHealthChecksUI();
                endpoints.MapControllers();
            });
        }
    }
}
