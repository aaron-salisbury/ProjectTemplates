using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DotNetFramework.Core.Logging
{
    /// <summary>
    /// Patterns & Practices implementation of ILogger.
    /// </summary>
    public class LoggerPNP : ILogger, IDisposable
    {
        public LogLevel MinimumLevel { get; private set; }

        internal LoggerPNPScope CurrentScope { get; set; } // Not using yet.

        private readonly LogWriter _writer;

        public LoggerPNP(LogLevel minimumLevel = LogLevel.Information, params TraceListener[] sinks)
        {
            MinimumLevel = minimumLevel;

            if (sinks == null || sinks.Length == 0)
            {
                sinks = [new ConsoleTraceListener()];
            }

            _writer = ConfigureLogWriter(sinks);
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return new LoggerPNPScope(this, state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return Convert.ToInt32(logLevel) >= Convert.ToInt32(MinimumLevel);
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

        public void Dispose()
        {
            _writer.Dispose();
        }

        private static LogWriter ConfigureLogWriter(IEnumerable<TraceListener> sinks)
        {
            // ref: http://web.archive.org/web/20210330115056/http://codebetter.com/davidhayden/2006/02/19/enterprise-library-2-0-logging-application-block/

            LogSource mainLogSource = new("MainLogSource", SourceLevels.All);
            mainLogSource.Listeners.Clear();
            mainLogSource.Listeners.AddRange(sinks);

            // Assigning a non-existent LogSource for Logging Application Block Special Sources we don’t care about.
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
                Severity = ConvertLogLevelToTraceEventType(logLevel),
                MachineName = Environment.MachineName,
                AppDomainName = AppDomain.CurrentDomain.FriendlyName
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
