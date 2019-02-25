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
        private readonly string _fileInfoPath;
        private readonly string _fileErrorPath;
        private readonly string _fileLogsFolder;
        private readonly object _locker = new object();

        public FileLogger(string fileInfoName, string fileErrorName, string fileLogsFolder)
        {
            _fileLogsFolder = fileLogsFolder;
            _fileInfoPath = Path.Combine(_fileLogsFolder, fileInfoName);
            _fileErrorPath = Path.Combine(_fileLogsFolder, fileErrorName);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Information || logLevel == LogLevel.Error;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_locker)
                {
                    // Choosing path depending on LogLevel
                    string path = " ";
                    if (logLevel == LogLevel.Error)
                    {
                        path = _fileErrorPath;
                    }
                    else
                    {
                        path = _fileInfoPath;
                    }

                    // Checking if file exist
                    if (!File.Exists(path))
                    {
                        using (File.Create(path))
                        {
                        }
                    }

                    // Reading old records from file and pushing new record
                    var stringLogs = File.ReadAllText(path);
                    var regex = new Regex(@"]$", RegexOptions.IgnoreCase);

                    if (!string.IsNullOrEmpty(stringLogs))
                    {
                        stringLogs = regex.Replace(stringLogs, "," + formatter(state, exception) + "]");
                    }
                    else
                    {
                        stringLogs = "[" + formatter(state, exception) + "]";
                    }

                    // Writing new records to file
                    File.WriteAllText(path, stringLogs);
                }
            }
        }
    }
}
