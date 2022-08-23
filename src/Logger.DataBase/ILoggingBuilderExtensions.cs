﻿using Logger.DataBase.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Logger.DataBase
{

    public static class ILoggingBuilderExtensions
    {

        public static void AddDataBaseLogger(this ILoggingBuilder builder, Action<LoggerSetting> action)
        {
            builder.Services.Configure(action);
            builder.Services.AddSingleton<ILoggerProvider, DataBaseLoggerProvider>();
        }
    }
}