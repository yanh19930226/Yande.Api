﻿using Common;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace Logger.DataBase
{
    public class DataBaseLogger : ILogger
    {

        private readonly string categoryName;


        private readonly IDHelper idHelper;

        private readonly LoggerSetting loggerSetting;



        public DataBaseLogger(string categoryName, LoggerSetting loggerSetting, IDHelper idHelper)
        {
            this.categoryName = categoryName;
            this.loggerSetting = loggerSetting;
            this.idHelper = idHelper;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default!;
        }


        public bool IsEnabled(LogLevel logLevel)
        {
            if (logLevel != LogLevel.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

            if (IsEnabled(logLevel))
            {

                if (state != null && state.ToString() != null)
                {
                    var logContent = state.ToString();

                    if (logContent != null)
                    {
                        if (exception != null)
                        {
                            var logMsg = new
                            {
                                message = logContent,
                                error = new
                                {
                                    exception?.Source,
                                    exception?.Message,
                                    exception?.StackTrace
                                }
                            };

                            logContent = JsonHelper.ObjectToJson(logMsg);
                        }


                        TLog log = new TLog()
                        {
                            Id = idHelper.GetId(),
                            CreateTime = DateTime.UtcNow,
                            Project = loggerSetting.Project,
                            MachineName = Environment.MachineName,
                            Category = categoryName,
                            Level = logLevel.ToString(),
                            Content = logContent
                        };


                        string logStr = JsonHelper.ObjectToJson(log);


                        string basePath = Directory.GetCurrentDirectory().Replace("\\", "/") + "/Logs/";

                        if (Directory.Exists(basePath) == false)
                        {
                            Directory.CreateDirectory(basePath);
                        }

                        var logPath = basePath + log.Id + ".log";

                        File.WriteAllText(logPath, logStr + Environment.NewLine, Encoding.UTF8);

                    }
                }

            }
        }
    }
}
