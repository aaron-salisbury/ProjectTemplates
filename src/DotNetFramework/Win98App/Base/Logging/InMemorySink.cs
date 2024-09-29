using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Win98App.Base.Logging
{
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class InMemorySink : CustomTraceListener
    {
        public event EventHandler<LogEmitEventArgs> LogEmitted;

        private readonly IList<string> _logs;
        public IList<string> Logs
        {
            get { return _logs; }
        }

        public int MaxLogsCount { get; set; }

        public InMemorySink(int maxLogsCount = 1000, ILogFormatter formatter = null)
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

            if (LogEmitted != null)
            {
                LogEmitted.Invoke(this, new LogEmitEventArgs() { LogMessage = message });
            }
        }

        public override void WriteLine(string message)
        {
            Write(message);
        }

        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (data is LogEntry && Formatter != null)
            {
                WriteLine(Formatter.Format(data as LogEntry));
            }
            else
            {
                WriteLine(data.ToString());
            }
        }

        private static ILogFormatter DefaultFormatter()
        {
            string textFormatterTemplate = "{timestamp(yyyy-MM-dd HH:mm:ss.ff)} UTC [{category}] {message}";

            return new TextFormatter(textFormatterTemplate);
        }
    }

    public class LogEmitEventArgs : EventArgs
    {
        public string LogMessage { get; set; }
    }
}
