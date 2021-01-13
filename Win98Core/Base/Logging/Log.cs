using System;

namespace Win98Core.Base.Logging
{
    public class Log
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss.FFFF";

        public enum LogEventLevels
        {
            Debug,
            Information,
            Warning,
            Error,
            Fatal
        }

        public LogEventLevels LogEventLevel { get; set; }

        public DateTime DateTime { get; set; }

        public Exception Exception { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            string postfix = Exception != null 
                ? $"{Environment.NewLine}\t{Exception.Message}" 
                : string.Empty;

            return $"{DateTime.ToString(DATE_FORMAT)} [{LogEventLevel}] {Message}{postfix}";
        }
    }
}
