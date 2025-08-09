using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotNetFramework.Core.Logging
{
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class InMemorySinkPNP : CustomTraceListener
    {
        public event EventHandler<LogEmitEventArgs> LogEmitted;

        private readonly IList<string> _logs;
        public IList<string> Logs
        {
            get { return _logs; }
        }

        public int MaxLogsCount { get; set; }

        public InMemorySinkPNP(int maxLogsCount = 1000, ILogFormatter formatter = null)
        {
            _logs = [];

            MaxLogsCount = maxLogsCount;
            Formatter = formatter ?? DefaultFormatter();
        }

        public override void Write(string message)
        {
            if (MaxLogsCount > 0 && Logs.Count >= MaxLogsCount)
            {
                Logs.RemoveAt(0);
            }

            Logs.Add(message);
        }

        public override void WriteLine(string message)
        {
            Write(message);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            DateTime timeStamp = DateTime.UtcNow;
            string message = data.ToString();
            LogLevel level = LogLevel.None;
            Exception exception = null;

            if (data is LogEntry logEntry)
            {
                timeStamp = logEntry.TimeStamp;
                level = MapTraceEventTypeToLogLevel(logEntry.Severity); // If not the LogEntryException subclass, then LogLevel wasn't explicitly saved.

                if (Formatter != null)
                {
                    message = Formatter.Format(logEntry);
                }
            }

            if (data is LogEntryException logEntryException)
            {
                level = logEntryException.LogLevel;
                exception = logEntryException.Exception;
            }

            WriteLine(message);

            LogEmitted?.Invoke(this, new LogEmitEventArgs()
                {
                    LogEvent = new()
                    {
                        TimeStamp = timeStamp,
                        Message = message,
                        Level = level,
                        Exception = exception
                    }
                });
        }

        private static ILogFormatter DefaultFormatter()
        {
            string textFormatterTemplate = "{timestamp(yyyy-MM-dd HH:mm:ss.ff)} UTC [{category}] {message}";

            return new TextFormatter(textFormatterTemplate);
        }

        private static LogLevel MapTraceEventTypeToLogLevel(TraceEventType trace)
        {
            return trace switch
            {
                TraceEventType.Verbose => LogLevel.Debug,
                TraceEventType.Information => LogLevel.Information,
                TraceEventType.Warning => LogLevel.Warning,
                TraceEventType.Error => LogLevel.Error,
                TraceEventType.Critical => LogLevel.Critical,
                _ => LogLevel.None,
            };
        }
    }

    public class LogEmitEventArgs : EventArgs
    {
        public LogEvent LogEvent { get; set; }
    }
}
