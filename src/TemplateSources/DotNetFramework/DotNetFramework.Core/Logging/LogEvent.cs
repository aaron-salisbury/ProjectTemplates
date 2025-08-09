using System;

namespace DotNetFramework.Core.Logging
{
    public class LogEvent
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
        public LogLevel Level { get; set; }
        public Exception Exception { get; set; }
    }
}
