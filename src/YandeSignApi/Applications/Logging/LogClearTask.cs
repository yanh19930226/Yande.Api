using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace Logger.LocalFile.Tasks
{
    /// <summary>
    /// 日志清空任务
    /// </summary>
    public class LogClearTask : BackgroundService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        /// <summary>
        /// LogClearTask
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        /// <param name="configuration"></param>
        public LogClearTask(
            IWebHostEnvironment webHostEnvironment, 
            IConfiguration configuration
         )
        {
            _webHostEnvironment= webHostEnvironment;
            _configuration= configuration;
        }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var basePath = Path.Combine(_webHostEnvironment.ContentRootPath, _configuration["LoggerSetting:LogFolder"]);

                    var LogSaveDays = Convert.ToInt32(_configuration["LoggerSetting:LogSaveDays"]);

                    if (Directory.Exists(basePath))
                    {
                        List<string> logPaths = IOHelper.GetFolderAllFiles(basePath).ToList();

                        if (logPaths.Count != 0)
                        {
                            foreach (var logPath in logPaths)
                            {
                                var fileInfo = new FileInfo(logPath);

                                if (fileInfo.CreationTime.AddMinutes(LogSaveDays) < DateTime.Now)
                                {
                                    File.Delete(logPath);
                                }
                            }
                        }
                        IOHelper.DeleteEmptyFolders(basePath);
                    }
                }
                catch
                {
                }
                //await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);

                await Task.Delay(1000 * 5, stoppingToken);
            }
        }
    }
}
