﻿using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using WinXPCore.Base.Logging;

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

            TargetWithLayout target = new InvocableMemoryTarget(INVOCABLE_MEMORY_TARGET_NAME, MEMORY_TARGET_NAME)
            {
                Layout = "${shortdate} ${time} [${level:format=FullName}] ${message}" // https://nlog-project.org/config/?tab=layout-renderers
            };

            config.AddRuleForAllLevels(target);

            LogManager.Configuration = config;
        }

        public static void Shutdown()
        {
            LogManager.Shutdown();
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

            if (invocableMemoryTarget != null && invocableMemoryTarget.MemoryTarget != null)
            {
                return invocableMemoryTarget.MemoryTarget.Logs as List<string>;
            }

            return null;
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

                Logger.Info(string.Format("Downloading log to {0}", tempLogFile));

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
                Logger.Error(e, string.Format("Failed to write error messages to file.{0}{1}", Environment.NewLine, e.Message));
                return null;
            }
        }
    }
}
