using NLog;
using NLog.Layouts;
using NLog.Targets;
using System;
using System.Collections.Generic;

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

        protected override void Write(LogEventInfo logEvent)
        {
            string renderedLogMessage = RenderLogEvent(Layout, logEvent);

            MemoryTargetWrite(renderedLogMessage);

            if (LogEmitted != null)
            {
                LogEmitted.Invoke(this, new LogEmitEventArgs()
                {
                    LogEvent = logEvent,
                    LogMessage = renderedLogMessage
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
    }

    public class LogEmitEventArgs : EventArgs
    {
        public LogEventInfo LogEvent { get; set; }
        public string LogMessage { get; set; }
    }
}
