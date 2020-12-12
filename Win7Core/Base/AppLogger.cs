using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace Win7Core.Base
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

        public void OpenLog()
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(PushMessagesToTempLogFile())
                {
                    UseShellExecute = true
                };

                Process.Start(processStartInfo);
                Logger.Information("Opened log file.");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }

        public string PushMessagesToTempLogFile()
        {
            string tempLogFile = null;

            try
            {
                string tempFileName = Path.GetTempFileName();
                FileInfo fileInfo = new FileInfo(tempFileName)
                {
                    Attributes = FileAttributes.Temporary
                };

                tempLogFile = Path.Combine(fileInfo.Directory.FullName, "log.txt");

                Logger.Information($"Downloading log to {tempLogFile}");

                if (File.Exists(tempLogFile))
                {
                    File.Delete(tempLogFile);
                }

                File.Move(tempFileName, tempLogFile);

                StreamWriter streamWriter = File.AppendText(tempLogFile);
                streamWriter.Write(InMemorySink.Messages);
                streamWriter.Flush();
                streamWriter.Dispose();

                return tempLogFile;
            }
            catch (Exception e)
            {
                File.Delete(tempLogFile);
                Logger.Error($"Failed to write error messages to file.{Environment.NewLine}{e.Message}");
                return null;
            }
        }
    }

    public class InMemorySink : ObservableObject, ILogEventSink
    {
        readonly ITextFormatter _textFormatter = new MessageTemplateTextFormatter("{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{Exception}", CultureInfo.InvariantCulture);

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
