using DotNetFramework.Core.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using ILogger = DotNetFramework.Core.Logging.ILogger;
using LogLevel = DotNetFramework.Core.Logging.LogLevel;

namespace WinXPApp.Base.Logging
{
    /// <summary>
    /// NLog implementation of ILogger.
    /// </summary>
    public class LoggerNLog : ILogger, IDisposable
    {
        public LogLevel MinimumLevel { get; private set; }

        private readonly Logger _logger;

        public LoggerNLog(LogLevel minimumLevel = LogLevel.Information, params Target[] sinks)
        {
            MinimumLevel = minimumLevel;

            _logger = ConfigureLogger(sinks);
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            string propertyName;
            object propertyValue;

            if (state is LoggerState loggerState)
            {
                if (loggerState.IsSingleProperty)
                {
                    propertyName = loggerState.SinglePropertyName;
                    propertyValue = loggerState.SinglePropertyValue;
                }
                else
                {
                    propertyName = nameof(LoggerState.PropertyValuesByNames);
                    propertyValue = loggerState.PropertyValuesByNames;
                }
            }
            else
            {
                propertyName = "State";
                propertyValue = state;
            }

            return _logger.PushScopeProperty(propertyName, propertyValue);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return Convert.ToInt32(logLevel) >= Convert.ToInt32(MinimumLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, DotNetFramework.Core.Logging.Func<TState, Exception, string> formatter)
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
                formattedMessage = formatter.Invoke(state, null);
            }

            NLog.LogLevel nlevel = ConvertLogLevelToNLogLogLevel(logLevel);

            _logger.Log(new LogEventInfo(nlevel, _logger.Name, formattedMessage)
            {
                TimeStamp = DateTime.UtcNow,
                Exception = exception,
            });
        }

        public void Dispose()
        {
            LogManager.Shutdown();
        }

        private static Logger ConfigureLogger(params Target[] sinks)
        {
            // ref: https://blog.elmah.io/nlog-tutorial-the-essential-guide-for-logging-from-csharp/

            if (sinks == null || sinks.Length == 0)
            {
                sinks = [new ConsoleTarget()];
            }

            LoggingConfiguration nLogConfig = new();
            foreach (Target sink in sinks)
            {
                nLogConfig.AddRuleForAllLevels(sink);
            }

            LogManager.Configuration = nLogConfig;

            return LogManager.GetCurrentClassLogger();
        }

        private static NLog.LogLevel ConvertLogLevelToNLogLogLevel(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => NLog.LogLevel.Trace,
                LogLevel.Debug => NLog.LogLevel.Debug,
                LogLevel.Information => NLog.LogLevel.Info,
                LogLevel.Warning => NLog.LogLevel.Warn,
                LogLevel.Error => NLog.LogLevel.Error,
                LogLevel.Critical => NLog.LogLevel.Fatal,
                LogLevel.None => NLog.LogLevel.Off,
                _ => NLog.LogLevel.Trace,
            };
        }
    }
}
