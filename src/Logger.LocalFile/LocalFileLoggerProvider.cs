using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Logger.LocalFile
{
    public class LocalFileLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, LocalFileLogger> loggers = new ConcurrentDictionary<string, LocalFileLogger>();

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, new LocalFileLogger(categoryName));
        }

        public void Dispose()
        {
            loggers.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
