using System;
using System.Collections.Generic;
using System.Linq;
using static Win98Core.Base.Logging.Log;

namespace Win98Core.Base.Logging
{
    public class AppLogger
    {
        public List<Log> Logs { get; set; } = new List<Log>();

        public IEnumerable<string> Messages => Logs.Select(l => l.Message);

        public string CombinedMessage => string.Join(Environment.NewLine, Messages.ToArray());

        public void AddLog(LogEventLevels logEventLevel, string message, Exception exception)
        {
            //var textLogger = new Microsoft.Practices.Composite.Logging.TextLogger();

            Logs.Add(new Log
            {
                DateTime = DateTime.Now,
                LogEventLevel = logEventLevel,
                Message = message,
                Exception = exception
            });
        }
    }
}
