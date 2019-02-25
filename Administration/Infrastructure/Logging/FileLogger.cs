using System;
using System.IO;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _path;
        private readonly object _locker = new object();

        public FileLogger(string path)
        {
            _path = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_locker)
                {
                    File.AppendAllText(_path, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
