﻿using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
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
        private readonly IList<string> _logs;
        public IList<string> Logs
        {
            get { return _logs; }
        }

        private int _maxLogsCount;
        public int MaxLogsCount
        {
            get { return _maxLogsCount; }
            set { _maxLogsCount = value; }
        }

        public delegate void MethodCall(IList<string> logs);

        private MethodCall _methodToCall;
        public MethodCall MethodToCall
        {
            get { return _methodToCall; }
            set { _methodToCall = value; }
        }

        public InvocableInMemoryTraceListener(int maxLogsCount = 1000, ILogFormatter formatter = null)
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
            if (data is LogEntry logEntry && Formatter != null)
            {
                WriteLine(Formatter.Format(logEntry));
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
}
