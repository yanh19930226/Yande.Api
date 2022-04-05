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

        // This method gets called by the runtime. Use this method to add services to the container.
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            #region Swagger
            app.UseOpenApi(); //����swagger����api�ĵ���Ĭ��·���ĵ� /swagger/v1/swagger.json��
            app.UseSwaggerUi3();//����Swagger UI������ܵ���(Ĭ��·��: /swagger). 
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}