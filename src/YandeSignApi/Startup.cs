using Dnc.Api.Throttle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YandeSignApi.Applications.Commons;
using YandeSignApi.Applications.Filters;
using YandeSignApi.Applications.SecurityAuthorization.RsaChecker;

namespace YandeSignApi
{
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
        public IConfiguration Configuration { get; }
        /// <summary>
        /// </summary>
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton(new AppSettingsHelper(Env.ContentRootPath));

            //Api����
            services.AddApiThrottle(options => {
                //����redis
                //���Cache��Storageʹ��ͬһ��redis������԰���������
                options.UseRedisCacheAndStorage(opts => {
                    opts.ConnectionString = "localhost,connectTimeout=5000,allowAdmin=false,defaultDatabase=0";
                    //opts.KeyPrefix = "apithrottle"; //ָ��������key����ǰ׺��Ĭ��Ϊapithrottle
                });
                //���Cache��Storageʹ�ò�ͬredis�⣬���԰���������
                options.UseRedisCache(opts =>
                {
                    opts.ConnectionString = "localhost,connectTimeout=5000,allowAdmin=false,defaultDatabase=0";
                });
                options.UseRedisStorage(opts =>
                {
                    opts.ConnectionString = "localhost,connectTimeout=5000,allowAdmin=false,defaultDatabase=1";
                });
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
                options.Filters.Add<ValidateModelStateFilter>();
                options.Filters.Add(typeof(ApiThrottleActionFilter));
            });

            services.AddRedisSetup();

            #region Swagger
            services.AddSwaggerSetup();
            #endregion

            services.AddAuthentication().AddAuthSecurityRsa();

            //services.AddSingleton(sp =>
            //{
            //    return new RsaOptions()
            //    {
            //        PrivateKey = Configuration.GetSection("RsaConfig")["PrivateKey"],
            //    };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Api����
            app.UseApiThrottle();

            #region Swagger
            app.UseCoreSwagger();
            #endregion

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
