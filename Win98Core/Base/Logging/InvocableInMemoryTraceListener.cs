using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System.Collections.Generic;
using System.Diagnostics;

namespace Win98Core.Base.Logging
{
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class InvocableInMemoryTraceListener : CustomTraceListener
    {
        public IList<string> Logs { get; } = new List<string>();

        public int MaxLogsCount { get; set; } = 1000;

        public delegate void MethodCall(IList<string> logs);

        public MethodCall MethodToCall { get; set; }

        public InvocableInMemoryTraceListener(ILogFormatter formatter)
        {
            Formatter = formatter;
        }

        public override void Write(string message)
        {
            if (MaxLogsCount > 0 && Logs.Count >= MaxLogsCount)
            {
                Logs.RemoveAt(0);
            }

            Logs.Add(message);

            if (MethodToCall != null)
            {
                MethodToCall.Invoke(Logs);
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
    }
}
