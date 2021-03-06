using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nacos.AspNetCore.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            #region Swagger
            services.AddOpenApiDocument(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "OrderApi";
                    document.Info.Description = "OrderApi";
                    document.Info.TermsOfService = "None";
                };
            });
            #endregion

            services.AddNacosAspNet(Configuration, "nacos");

            //services.AddNacosAspNet(p =>
            //{

            //});

            services.AddScoped<NacosDiscoveryDelegatingHandler>();

            //ProductService
            services.AddHttpClient(ServiceName.ProductService, client =>
            {
                client.BaseAddress = new Uri($"http://{ServiceName.ProductService}");
            }).AddHttpMessageHandler<NacosDiscoveryDelegatingHandler>();

            //CartService
            services.AddHttpClient(ServiceName.CartService, client =>
            {
                client.BaseAddress = new Uri($"http://{ServiceName.CartService}");
            }).AddHttpMessageHandler<NacosDiscoveryDelegatingHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            #region Swagger
            app.UseOpenApi(); //添加swagger生成api文档（默认路由文档 /swagger/v1/swagger.json）
            app.UseSwaggerUi3();//添加Swagger UI到请求管道中(默认路由: /swagger). 
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
