using AspNetCoreRateLimit;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YandeSignApi.Applications.Commons;
using YandeSignApi.Applications.Filters;
using YandeSignApi.Applications.HealthChecks;
using YandeSignApi.Applications.Middlewares;
using YandeSignApi.Applications.SecurityAuthorization.RsaChecker;

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
            services.AddSingleton(new AppSettingsHelper(Env.ContentRootPath));
            services.AddControllers(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
                options.Filters.Add<ValidateModelStateFilter>();
            });

            services.AddCustomHealthCheck(this.Configuration);
            services.AddHealthChecksUI(setupSettings =>
            {
                //????վ?㣬???????Ӷ?????UI?л???վ???ڵļ???????????ʾ??Ҳ??ͨ???????ļ?ʵ?֣?
                setupSettings.AddHealthCheckEndpoint(name: "localhost-5000", uri: "http://localhost:5000/health");
                //?????????쳣????ʱ??????Ϣ??api
                setupSettings.AddWebhookNotification("messageWebhook",
                    uri: "http://localhost:5008/WeatherForecast/message",
                    payload: "{ \"message\": \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
                    restorePayload: "{ \"message\": \"[[LIVENESS]] is back to life\"}");
                setupSettings.SetMinimumSecondsBetweenFailureNotifications(60);
                setupSettings.SetEvaluationTimeInSeconds(10);
            }).AddSqlServerStorage(Configuration["HealthStorageConnectionString"]);//???ݿ??־û?

            services.AddRedisSetup();

            #region Swagger
            services.AddSwaggerSetup();
            #endregion

            #region Rsa????
            services.AddAuthentication().AddAuthSecurityRsa();
            //services.AddSingleton(sp =>
            //{
            //    return new RsaOptions()
            //    {
            //        PrivateKey = Configuration.GetSection("RsaConfig")["PrivateKey"],
            //    };
            //});
            #endregion

            #region ????
            //??Ҫ?Ӽ????????ļ?appsettings.json
            services.AddOptions();
            //??Ҫ?洢???????Ƽ???????ip????
            services.AddMemoryCache();

            //??appsettings.json?м??س???????
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //??appsettings.json?м???Ip????
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            //ע?????????͹????洢
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            //???ã?????????????????Կ????????
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            #endregion
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

            #region ?м???
            // ?쳣?????м???
            app.UseExceptionHandlerMidd();
            // ??¼?????뷵?????? (ע?⿪??Ȩ?ޣ???Ȼ?????޷?д??)
            app.UseRequestResponseLog();
            // ??¼ip???? (ע?⿪??Ȩ?ޣ???Ȼ?????޷?д??)
            app.UseIpLogMildd();
            #endregion

            #region Swagger
            app.UseCoreSwagger();
            #endregion

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseIpRateLimiting();

            app.UseClientRateLimiting();

            app.UseHealthChecksUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    //Predicate = _ => true,
                    //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI();

                endpoints.MapControllers();
            });
        }
    }
}
