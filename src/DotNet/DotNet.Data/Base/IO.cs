using Microsoft.Extensions.Logging;

namespace DotNet.Data.Base;

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

    public static void DeleteFile(string fullFilePath, ILogger logger)
    {
        try
        {
            if (File.Exists(fullFilePath))
            {
                File.Delete(fullFilePath);
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
}
