using DotNetFrameworkToolkit.Core;
using DotNetFrameworkToolkit.Core.Extensions;
using DotNetFrameworkToolkit.Modules.DataAccess.FileSystem;
using DotNetFrameworkToolkit.Modules.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Win7App.Base;
using Win7App.Base.MvvmInput;
using Win7App.Base.Services;

namespace Win7App.ViewModels;

public class LogsViewModel : BaseViewModel
{
    public RelayCommand DownloadCommand { get; }

    private List<string> _errorLogs;
    public List<string> ErrorLogs
    {
        get { return _errorLogs; }
        set
        {
            SetField(ref _errorLogs, value, nameof(ErrorLogs));
            CombinedErrorMessage = string.Join(Environment.NewLine, [.. value]);
        }
    }

    private string _combinedErrorMessage;
    public string CombinedErrorMessage
    {
        get { return _combinedErrorMessage; }
        set { SetField(ref _combinedErrorMessage, value, nameof(CombinedErrorMessage)); }
    }

    private readonly InMemorySinkPNP _logSource;
    private readonly ILogger _logger;
    private readonly IFileSystemAccess _fileSystemAccess;
    private readonly IAgnosticDispatcher _dispatcher;

    public LogsViewModel(InMemorySinkPNP logSource, ILogger logger, IAgnosticDispatcher dispatcher, IFileSystemAccess fileSystemAccess)
    {
        _logSource = logSource;
        _logger = logger;
        _fileSystemAccess = fileSystemAccess;
        _dispatcher = dispatcher;
        _errorLogs = new List<string>(_logSource.Logs);

        DownloadCommand = new RelayCommand(async () => await DownloadLogAsync(), () => !IsBusy);
    }

    public void WireErrors()
    {
        _logSource.LogEmitted += LogSource_LogEmitted;
    }

    private async Task DownloadLogAsync()
    {
        ProcessResult<string> appDirectoryResult = _fileSystemAccess.GetAppDirectoryPath();
        if (!appDirectoryResult.IsSuccessful)
        {
            _logger.LogError(appDirectoryResult.Error, "Could not get app directory path to write logs to.");
            return;
        }

        string appDirectoryPath = appDirectoryResult.Value;
        string logsPath = Path.Combine(appDirectoryPath, "Logs");
        string fileName = $"Logs_{DateTimeExtensions.ToTimeStamp(DateTime.Now)}.txt";

        ProcessResult<bool> writeResult = _fileSystemAccess.WriteFile(_errorLogs, fileName, logsPath);
        if (writeResult.IsSuccessful)
        {
            _logger.LogInformation("Wrote log file '{0}' to '{1}'", fileName, logsPath);

            try
            {
                Process.Start(new ProcessStartInfo(Path.Combine(logsPath, fileName))
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to open file.");
            }
        }
    }

    private void LogSource_LogEmitted(object sender, LogEmitEventArgs e)
    {
        _dispatcher.PostBackground(() => ErrorLogs = [.. _logSource.Logs]);
    }
}
