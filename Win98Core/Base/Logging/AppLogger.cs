﻿using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Filters;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Win98Core.Base.Logging;

namespace Win98Core.Base
{
    // http://codebetter.com/davidhayden/2006/02/19/enterprise-library-2-0-logging-application-block/
    public static class AppLogger
    {
        private static readonly LogWriter _writer;

        private static readonly InvocableInMemoryTraceListener _inMemoryTraceListener;

        public enum LogCategories
        {
            Debug,
            Information,
            Warning,
            Error,
            Fatal
        }

        static AppLogger()
        {
            _inMemoryTraceListener = ConfigureInMemoryTraceListener();
            _writer = ConfigureLogWriter(_inMemoryTraceListener);
        }

        private static InvocableInMemoryTraceListener ConfigureInMemoryTraceListener()
        {
            // The formatter is responsible for the look of the message. 
            // Notice the tokens: {timestamp}, {newline}, {message}, {category}
            string textFormatterTemplate = "Timestamp: { timestamp }{ newline}" + "Message: { message} { newline}" + "Category: { category} { newline}";
            TextFormatter formatter = new TextFormatter(textFormatterTemplate);

            // Log messages to an in memory collection.
            return new InvocableInMemoryTraceListener(formatter);
        }

        private static LogWriter ConfigureLogWriter(InvocableInMemoryTraceListener inMemoryTraceListener)
        {
            // My collection of TraceListeners. I am only using one. Could add more.
            LogSource mainLogSource = new LogSource("MainLogSource", SourceLevels.All);
            mainLogSource.Listeners.Add(inMemoryTraceListener);

            // Assigning a non-existant LogSource for Logging Application Block Special Sources I don’t care about.
            LogSource nonExistantLogSource = new LogSource("Empty");

            // I want all messages with a category of “Error” or “Debug” to get distributed to all TraceListeners in my mainLogSource.
            IDictionary<string, LogSource> traceSources = new Dictionary<string, LogSource>();
            traceSources.Add(LogCategories.Error.ToString(), mainLogSource);
            traceSources.Add(LogCategories.Debug.ToString(), mainLogSource);

            // Gluing it all together.
            // No filters at this time.
            // Not yet logging a couple of the Special Sources: 
            // All Events and Events not using “Error” or “Debug” categories.
            return new LogWriter(
                new ILogFilter[0],
                traceSources,
                nonExistantLogSource,
                nonExistantLogSource,
                mainLogSource,
                LogCategories.Error.ToString(),
                false,
                true);
        }

        /// <summary>
        /// Writes a message to the log using the specified category.
        /// </summary>
        public static void Write(string message, LogCategories category)
        {
            LogEntry entry = new LogEntry();
            entry.Categories.Add(category.ToString());
            entry.Message = message;
            _writer.Write(entry);
        }

        /// <summary>
        /// Do something when new log is added.
        /// </summary>
        /// <param name="methodCall">Method that accepts IList<string>, which will be logs.</param>
        public static void SetTargetInvoking(InvocableInMemoryTraceListener.MethodCall methodCall)
        {
            if (_inMemoryTraceListener != null)
            {
                _inMemoryTraceListener.MethodToCall = methodCall;
            }
        }

        public static List<string> GetLogs()
        {
            return _inMemoryTraceListener?.Logs as List<string>;
        }

        public static string GetMessages()
        {
            return string.Join(Environment.NewLine, GetLogs().ToArray());
        }

        public static void OpenLog()
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(PushMessagesToTempLogFile())
                {
                    UseShellExecute = true
                };

                Process.Start(processStartInfo);
                Write("Opened log file.", LogCategories.Information);
            }
            catch (Exception e)
            {
                Write(e.Message, LogCategories.Error);
            }
        }

        public static string PushMessagesToTempLogFile()
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

                Write($"Downloading log to {tempLogFile}", LogCategories.Information);

                if (File.Exists(tempLogFile))
                {
                    File.Delete(tempLogFile);
                }

                File.Move(tempFileName, tempLogFile);

                StreamWriter streamWriter = File.AppendText(tempLogFile);
                streamWriter.Write(GetMessages());
                streamWriter.Flush();
                streamWriter.Dispose();

                return tempLogFile;
            }
            catch (Exception e)
            {
                File.Delete(tempLogFile);
                Write($"Failed to write error messages to file.{Environment.NewLine}{e.Message}", LogCategories.Error);
                return null;
            }
        }
    }
}
