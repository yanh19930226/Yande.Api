﻿using Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YandeSignApi.Applications.Logging;

namespace Logger.LocalFile.Tasks
{
    public class LogClearTask : BackgroundService
    {

        private readonly int saveDays;


        public LogClearTask(IOptionsMonitor<LoggerSetting> config)
        {
            saveDays = config.CurrentValue.SaveDays;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {

                    string basePath = Directory.GetCurrentDirectory().Replace("\\", "/") + "/Logs/";

                    if (Directory.Exists(basePath))
                    {
                        List<string> logPaths = IOHelper.GetFolderAllFiles(basePath).ToList();

                        var deleteTime = DateTime.UtcNow.AddDays(-1 * saveDays);

                        if (logPaths.Count != 0)
                        {
                            foreach (var logPath in logPaths)
                            {
                                var fileInfo = new FileInfo(logPath);

                                if (fileInfo.CreationTimeUtc < deleteTime)
                                {
                                    File.Delete(logPath);
                                }

                            }
                        }
                    }

                }
                catch
                {
                }

                await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);
            }
        }

    }
}
