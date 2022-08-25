using Logger.LocalFile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YandeSignApi.Applications.Logs;

namespace YandeSignApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            try
            {
                #region 写入数据库需要
                ////确保NLog.config中连接字符串与appsettings.json中同步
                //NLogUtil.EnsureNlogConfig("NLog.config"); 
                #endregion

                //其他项目启动时需要做的事情
                NLogUtil.WriteFileLog(NLog.LogLevel.Trace, LogType.Web, "网站启动", "网站启动成功");
                host.Run();
            }
            catch (Exception ex)
            {
                //使用nlog写到本地日志文件（万一数据库没创建/连接成功）
                NLogUtil.WriteFileLog(NLog.LogLevel.Error, LogType.Web, "网站启动", "初始化数据异常", ex);
                throw;
            }
        }
        /// <summary>
        /// CreateHostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext, logging) =>
            {
                #region Nlog配置

                //移除已经注册的其他日志处理程序
                logging.ClearProviders();

                //设置最小的日志级别
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Error);

                //NLog设置文件存储路径
                var appBasePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), hostingContext.Configuration["LoggerSetting:LogFolder"]);
                NLog.GlobalDiagnosticsContext.Set("appbasepath", appBasePath);

                #endregion
            })
            .UseNLog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
