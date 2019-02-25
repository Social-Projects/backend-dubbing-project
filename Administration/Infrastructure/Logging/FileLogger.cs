using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
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
                    if (!File.Exists(_path))
                    {
                        using (File.Create(_path))
                        {
                        }
                    }

                    var stringLogs = File.ReadAllText(_path);
                    var regex = new Regex(@"]$", RegexOptions.IgnoreCase);

                    if (!string.IsNullOrEmpty(stringLogs))
                    {
                        stringLogs = regex.Replace(stringLogs, "," + formatter(state, exception) + "]");
                    }
                    else
                    {
                        stringLogs = "[" + formatter(state, exception) + "]";
                    }

                    File.WriteAllText(_path, stringLogs);
                }
            }
        }
    }
}
