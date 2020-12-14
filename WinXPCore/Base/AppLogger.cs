using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace WinXPCore.Base
{
    public class AppLogger : LoggableObject
    {
        // https://blog.elmah.io/nlog-tutorial-the-essential-guide-for-logging-from-csharp/
        // https://github.com/NLog/NLog/wiki/Tutorial

        private const string MEMORY_TARGET_NAME = "MemoryTarget";
        private const string INVOCABLE_MEMORY_TARGET_NAME = "InvocableMemoryTarget";

        private static InvocableMemoryTarget GetInvocableMemoryTarget()
        {
            return LogManager.Configuration.FindTargetByName<InvocableMemoryTarget>(INVOCABLE_MEMORY_TARGET_NAME);
        }

        public static void InitializeLogger()
        {
            LoggingConfiguration config = new LoggingConfiguration();

            Target target = new InvocableMemoryTarget(INVOCABLE_MEMORY_TARGET_NAME, MEMORY_TARGET_NAME);

            config.AddRuleForAllLevels(target);

            LogManager.Configuration = config;
        }

        /// <summary>
        /// Do something when new log is added.
        /// </summary>
        /// <param name="methodCall">Method that accepts IList<string>, which will be logs.</param>
        public static void SetTargetInvoking(InvocableMemoryTarget.MethodCall methodCall)
        {
            InvocableMemoryTarget invocableMemoryTarget = GetInvocableMemoryTarget();

            if (invocableMemoryTarget != null)
            {
                invocableMemoryTarget.MethodToCall = methodCall;
            }
        }

        public static List<string> GetLogs()
        {
            InvocableMemoryTarget invocableMemoryTarget = GetInvocableMemoryTarget();

            return invocableMemoryTarget?.MemoryTarget?.Logs as List<string>;
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
                Logger.Info("Opened log file.");
            }
            catch (Exception e)
            {
                Logger.Error(e);
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

                Logger.Info($"Downloading log to {tempLogFile}");

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
                Logger.Error(e, $"Failed to write error messages to file.{Environment.NewLine}{e.Message}");
                return null;
            }
        }
    }
}
