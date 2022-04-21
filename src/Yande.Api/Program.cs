using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nacos.YamlParser;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yande.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var s=CreateHostBuilder(args).Build();
            s.Run();
        }

        /// <summary>
        /// Serilog日志模板
        /// </summary>
        static string serilogDebug = System.Environment.CurrentDirectory + "\\Log\\Debug\\.log";
        static string serilogInfo = System.Environment.CurrentDirectory + "\\Log\\Info\\.log";
        static string serilogWarn = System.Environment.CurrentDirectory + "\\Log\\Warning\\.log";
        static string serilogError = System.Environment.CurrentDirectory + "\\Log\\Error\\.log";
        static string serilogFatal = System.Environment.CurrentDirectory + "\\Log\\Fatal\\.log";

        static string SerilogOutputTemplate = "{NewLine}时间:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志等级:{Level}{NewLine}所在类:{SourceContext}{NewLine}日志信息:{Message}{NewLine}{Exception}";


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory()) 
            .ConfigureAppConfiguration((context, builder) =>
            {
                var c = builder.Build();
                //builder.AddNacosV2Configuration(c.GetSection("NacosConfig"));
                // builder.AddNacosV2Configuration(c.GetSection("NacosConfig"), Nacos.IniParser.IniConfigurationStringParser.Instance);
                //builder.AddNacosV2Configuration(c.GetSection("NacosConfig"), YamlConfigurationStringParser.Instance);

            }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog((context, logger) =>//注册Serilog
                    {
                        logger.ReadFrom.Configuration(context.Configuration);
                        logger.Enrich.FromLogContext();
                        logger.WriteTo.Console();  // 输出到Console控制台
                        // 输出到配置的文件日志目录
                        logger.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug).WriteTo.Async(a => a.File(serilogDebug, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information).WriteTo.Async(a => a.File(serilogInfo, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning).WriteTo.Async(a => a.File(serilogWarn, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error).WriteTo.Async(a => a.File(serilogError, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                            .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal).WriteTo.Async(a => a.File(serilogFatal, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)));
                    })
                    .UseUrls("http://*:8880");
                });
    }
}
