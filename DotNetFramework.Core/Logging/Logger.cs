using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotNetFramework.Core.Logging
{
    public class Logger : ILogger
    {
        public LoggerScope CurrentScope { get; set; } // Not using yet.

        public readonly LogLevel _minimumLevel;
        private readonly LogWriter _writer;

        public Logger(LogLevel minimumLevel = LogLevel.Information, params TraceListener[] sinks)
        {
            if (sinks == null || sinks.Length == 0)
            {
                sinks = [new DefaultTraceListener()];
            }

            _writer = ConfigureLogWriter(sinks);
            _minimumLevel = minimumLevel;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return new LoggerScope(this, state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return Convert.ToInt32(logLevel) >= Convert.ToInt32(_minimumLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            string formattedMessage;
            if (formatter == null)
            {
                formattedMessage = state.ToString();
            }
            else
            {
                formattedMessage = formatter.Invoke(state, exception);
            }

            _writer.Write(BuildLogEntry(logLevel, eventId, formattedMessage));
        }

        private static LogWriter ConfigureLogWriter(IEnumerable<TraceListener> sinks)
        {
            LogSource mainLogSource = new("MainLogSource", SourceLevels.All);
            mainLogSource.Listeners.AddRange(sinks);

            // Assigning a non-existent LogSource for Logging Application Block Special Sources I don’t care about.
            LogSource nonExistentLogSource = new("Empty");

            // All messages, of any category, get distributed to all TraceListeners in mainLogSource.
            IDictionary<string, LogSource> traceSources = new Dictionary<string, LogSource>();
            foreach (LogLevel logCategory in Enum.GetValues(typeof(LogLevel)))
            {
                traceSources.Add(logCategory.ToString(), mainLogSource);
            }

            string defaultCategory = LogLevel.Error.ToString();

            // No filters at this time.
            return new LogWriter([], traceSources, nonExistentLogSource, nonExistentLogSource, mainLogSource, defaultCategory, false, true);
        }

        private static LogEntry BuildLogEntry(LogLevel logLevel, EventId eventId, string message)
        {
            LogEntry logEntry = new()
            {
                TimeStamp = DateTime.UtcNow,
                Message = message,
                Categories = [logLevel.ToString()],
                Severity = ConvertLogLevelToTraceEventType(logLevel)
            };

            if (eventId != null)
            {
                logEntry.EventId = eventId.Id;
            }

            return logEntry;
        }

        private static TraceEventType ConvertLogLevelToTraceEventType(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace or LogLevel.Debug => TraceEventType.Verbose,
                LogLevel.Information => TraceEventType.Information,
                LogLevel.Warning => TraceEventType.Warning,
                LogLevel.Error => TraceEventType.Error,
                LogLevel.Critical => TraceEventType.Critical,
                LogLevel.None => TraceEventType.Verbose,
                _ => TraceEventType.Verbose,
            };
        }
    }
}
