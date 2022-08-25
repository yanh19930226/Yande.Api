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
            //    //���վ�㣬������Ӷ�����UI�л��վ���ڵļ��������ʾ��Ҳ��ͨ�������ļ�ʵ�֣�
            //    setupSettings.AddHealthCheckEndpoint(name: "localhost-5000", uri: "http://localhost:5000/health");
            //    //�������쳣���ʱ������Ϣ��api
            //    setupSettings.AddWebhookNotification("messageWebhook",
            //        uri: "http://localhost:5008/WeatherForecast/message",
            //        payload: "{ \"message\": \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
            //        restorePayload: "{ \"message\": \"[[LIVENESS]] is back to life\"}");
            //    setupSettings.SetMinimumSecondsBetweenFailureNotifications(60);
            //    setupSettings.SetEvaluationTimeInSeconds(10);
            //}).AddSqlServerStorage(Configuration["HealthStorageConnectionString"]);//���ݿ�־û�

            services.AddRedisSetup();

            #region ��̨����

            services.AddHostedService<LogClearTask>();

            #endregion

            #region ע����ŷ���

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

            #region ע���ļ�����

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

            #region Rsa����
            services.AddAuthentication().AddAuthSecurityRsa();
            //services.AddSingleton(sp =>
            //{
            //    return new RsaOptions()
            //    {
            //        PrivateKey = Configuration.GetSection("RsaConfig")["PrivateKey"],
            //    };
            //});
            #endregion

            #region ����
            //��Ҫ�Ӽ��������ļ�appsettings.json
            services.AddOptions();
            //��Ҫ�洢�������Ƽ�������ip����
            services.AddMemoryCache();

            //��appsettings.json�м��س�������
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //��appsettings.json�м���Ip����
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            //ע��������͹���洢
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            //���ã�����������������Կ��������
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

            #region �м��
            // �쳣�����м��
            app.UseExceptionHandlerMidd();
            // ��¼�����뷵������ (ע�⿪��Ȩ�ޣ���Ȼ�����޷�д��)
            app.UseRequestResponseLog();
            // ��¼ip���� (ע�⿪��Ȩ�ޣ���Ȼ�����޷�д��)
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
