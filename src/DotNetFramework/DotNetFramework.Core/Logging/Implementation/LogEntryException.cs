using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;

namespace DotNetFramework.Core.Logging
{
    public class LogEntryException : LogEntry
    {
        public Exception Exception { get; set; }
        public LogLevel LogLevel { get; set; }
    }
}
