using Autofac;
using DotXxlJob.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nacos.AspNetCore.V2;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yande.Api.Common;
using Yande.Api.Jobs;
using Yande.Api.Models;
using Yande.Core.AppSettings;
using Yande.Core.Redis;
using Yande.Middleware;
using YandeSignApi.Applications.Redis;

namespace Yande.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddSingleton(new AppHelper(Configuration));

            // 配置启动Redis服务，虽然可能影响项目启动速度，但是不能在运行的时候报错，所以是合理的
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                //获取连接字符串
                string redisConfiguration = "114.55.177.197,connectTimeout=1000,connectRetry=1,syncTimeout=10000,DefaultDatabase=8";

                var configuration = ConfigurationOptions.Parse(redisConfiguration, true);

                configuration.ResolveDns = true;

                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddTransient<IRedisOperationRepository, RedisOperationRepository>();

            services.AddControllers();

            services.Configure<UserInfo>(Configuration.GetSection("UserInfo"));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            #region xxljob
            //services.AddDefaultXxlJobHandlers();
            //services.AddXxlJobExecutor(Configuration);
            //services.AddSingleton<IJobHandler, DemoJobHandler>(); // 添加自定义的jobHandler
            //services.AddAutoRegistry(); // 自动注册 
            #endregion

            //services.AddNacosAspNet(Configuration, "nacos");

            #region Swagger
            services.AddOpenApiDocument(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Yande.Api";
                    document.Info.Description = "Yande.Api";
                    document.Info.TermsOfService = "None";
                };
            });
            #endregion

            services.AddMiniProfiler(options =>
     options.RouteBasePath = "/profiler"
    );

            #region 设置跨域
            services.AddCors(options => options.AddPolicy("CorsPolicy",
             builder =>
             {
                 builder.AllowAnyMethod()
                     .AllowAnyHeader()
                     .SetIsOriginAllowed(_ => true) // =AllowAnyOrigin()
                     .AllowCredentials();
             }));
            #endregion
        }

        #region Autofac
        public void ConfigureContainer(ContainerBuilder container)
        {
            container.RegisterModule(new AutofacRegister());
        }
        #endregion


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region xxljob
            //// 注册执行器中间件
            //app.UseXxlJobExecutor(); 
            #endregion

            #region Swagger
            app.UseOpenApi(); //添加swagger生成api文档（默认路由文档 /swagger/v1/swagger.json）
            app.UseSwaggerUi3();//添加Swagger UI到请求管道中(默认路由: /swagger). 
            #endregion

            app.UseMiniProfiler();

            app.UseRouting();

            app.UseAuthorization();

            //允许跨域请求
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
