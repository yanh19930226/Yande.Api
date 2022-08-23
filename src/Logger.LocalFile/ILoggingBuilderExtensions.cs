using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logger.LocalFile
{
    public static class ILoggingBuilderExtensions
    {
        public static void AddLocalFileLogger(this ILoggingBuilder builder, Action<LoggerSetting> action)
        {
            builder.Services.Configure(action);
            builder.Services.AddSingleton<ILoggerProvider, LocalFileLoggerProvider>();
        }
    }
}
