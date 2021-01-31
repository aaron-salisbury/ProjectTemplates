using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;

namespace Win10Core.Base
{
    public class AppLogger
    {
        public ILogger Logger { get; set; }
        public InMemorySink InMemorySink { get; set; }

        public AppLogger()
        {
            InMemorySink = new InMemorySink();

            Logger = new LoggerConfiguration()
                .WriteTo.Sink(InMemorySink)
                .CreateLogger();
        }
    }

    public class InMemorySink : ObservableObject, ILogEventSink
    {
        readonly ITextFormatter _textFormatter = new MessageTemplateTextFormatter("{Timestamp:yyyy-MM-dd HH:mm:ss:ff} [{Level}] {Message}{Exception}", CultureInfo.InvariantCulture);

        public ConcurrentQueue<string> Events { get; } = new ConcurrentQueue<string>();

        private string _messages;
        public string Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                RaisePropertyChanged("Messages");
            }
        }

        public void Emit(LogEvent logEvent)
        {
            if (logEvent == null) { throw new ArgumentNullException(nameof(logEvent)); }

            StringWriter renderSpace = new StringWriter();
            _textFormatter.Format(logEvent, renderSpace);
            string formattedLogEvent = renderSpace.ToString();
            Events.Enqueue(formattedLogEvent);

            if (Events.Count > 1)
            {
                Messages += (Environment.NewLine + formattedLogEvent);
            }
            else
            {
                Messages += formattedLogEvent;
            }
        }
    }
}
