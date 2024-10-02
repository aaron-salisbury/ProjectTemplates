using DotNetFramework.Core.Logging;
using NLog;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;
using LogLevel = DotNetFramework.Core.Logging.LogLevel;

namespace WinXPApp.Base.Logging
{
    [Target(nameof(InMemorySink))]
    public class InMemorySink : TargetWithLayout
    {
        private const string DEFAULT_LAYOUT = "${shortdate} ${time} [${level:format=FullName}] ${message}"; // https://nlog-project.org/config/?tab=layout-renderers

        public event EventHandler<LogEmitEventArgs> LogEmitted;

        public IList<string> Logs
        {
            get { return _memoryTarget.Logs; }
        }

        private readonly MemoryTarget _memoryTarget;
        private readonly Layout _layout;

        public InMemorySink(int maxLogsCount = 1000, string name = null, Layout formatter = null)
        {
            Name = name ?? nameof(InMemorySink);
            Layout = formatter ?? DEFAULT_LAYOUT;

            _memoryTarget = new()
            {
                MaxLogsCount = maxLogsCount,
                Name = $"{this.Name}{nameof(MemoryTarget)}",
                Layout = this.Layout
            };
        }

        protected override void Write(LogEventInfo logEventInfo)
        {
            DateTime timeStamp = logEventInfo.TimeStamp;
            string message = RenderLogEvent(Layout, logEventInfo);
            LogLevel level = MapNLogLevelToLogLevel(logEventInfo.Level);
            Exception exception = logEventInfo.Exception;

            MemoryTargetWrite(message);

            if (LogEmitted != null)
            {
                LogEmitted.Invoke(this, new LogEmitEventArgs()
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
        }

        private void MemoryTargetWrite(string renderedLogMessage)
        {
            if (_memoryTarget.MaxLogsCount > 0 && _memoryTarget.Logs.Count >= _memoryTarget.MaxLogsCount)
            {
                _memoryTarget.Logs.RemoveAt(0);
            }

            _memoryTarget.Logs.Add(renderedLogMessage);
        }

        private static LogLevel MapNLogLevelToLogLevel(NLog.LogLevel nLogLevel)
        {
            return nLogLevel.Ordinal switch
            {
                0 => LogLevel.Trace,
                1 => LogLevel.Debug,
                2 => LogLevel.Information,
                3 => LogLevel.Warning,
                4 => LogLevel.Error,
                5 => LogLevel.Critical,
                6 => LogLevel.None,
                _ => LogLevel.None,
            };
        }
    }

    public class LogEmitEventArgs : EventArgs
    {
        public LogEvent LogEvent { get; set; }
    }
}
