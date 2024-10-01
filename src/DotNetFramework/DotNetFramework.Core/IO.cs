using DotNetFramework.Core.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace DotNetFramework.Core
{
    public static class IO
    {
        public static string GetAppDirectoryPath()
        {
            string localAppDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string thisAppName = Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName);
            string appDirectory = Path.Combine(localAppDirectory, thisAppName);

            Directory.CreateDirectory(appDirectory);

            return appDirectory;
        }

        public static bool WriteFile(ILogger logger, IEnumerable<string> contentLines, string fileName, string directoryPath = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                else
                {
                    directoryPath = GetAppDirectoryPath();
                }

                string fullPath = Path.Combine(directoryPath, fileName);

                using (StreamWriter outputFile = new(fullPath))
                {
                    foreach (string line in contentLines)
                    {
                        outputFile.WriteLine(line);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to write file.");
                return false;
            }
        }

        public static void DeleteFile(string filePath, ILogger logger)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                else
                {
                    logger.LogWarning("Attempted to delete a file that does not exist.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to delete file.");
            }
        }

        public static void OpenFile(string filePath, ILogger logger)
        {
            try
            {
                Process.Start(new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to open file.");
            }
        }

        public static string GetEmbeddedResourceText(Assembly assemblyEmbeddedIn, string filePath, ILogger logger)
        {
            string result = string.Empty;

            try
            {
                using (Stream stream = assemblyEmbeddedIn.GetManifestResourceStream(filePath))
                using (StreamReader streamReader = new(stream))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve embedded text.");
            }

            return result;
        }
    }
}
