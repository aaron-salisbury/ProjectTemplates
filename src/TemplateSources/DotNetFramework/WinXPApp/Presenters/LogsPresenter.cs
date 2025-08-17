using DotNetFrameworkToolkit.Core;
using DotNetFrameworkToolkit.Core.Extensions;
using DotNetFrameworkToolkit.Modules.DataAccess.FileSystem;
using DotNetFrameworkToolkit.Modules.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WinXPApp.Base.MVP;
using WinXPApp.Views;

namespace WinXPApp.Presenters
{
    internal class LogsPresenter : Presenter
    {
        private readonly InMemorySinkPNP _logSource;
        private readonly ILogger _logger;
        private readonly IFileSystemAccess _fileSystemAccess;
        private List<string> _errorLogs;
        private LogsView _view;

        public LogsPresenter(InMemorySinkPNP logSource, ILogger logger, IFileSystemAccess fileSystemAccess)
        {
            _logSource = logSource;
            _logger = logger;
            _fileSystemAccess = fileSystemAccess;
        }

        internal override void Setup(UserControl view)
        {
            _view = (LogsView)view;

            _view.DownloadCommand += View_DownloadCommand;

            RefreshErrors();
            _logSource.LogEmitted += LogSource_LogEmitted; // So the logs view refreshes with new errors while the logs view is in focus.
        }

        internal override void Dismiss()
        {
            if (_view != null)
            {
                _view.DownloadCommand -= View_DownloadCommand;
            }

            if (_logSource != null)
            {
                _logSource.LogEmitted -= LogSource_LogEmitted;
            }
        }

        private void RefreshErrors()
        {
            if (_view != null)
            {
                _errorLogs = new List<string>(_logSource.Logs);

                _view.UpdateLogs(_errorLogs);
            }
        }

        private void LogSource_LogEmitted(object sender, LogEmitEventArgs e)
        {
            //TODO: Right now the logs view simply joins all the messages into one long sting,
            //  but could use the data off of LogEmitEventArgs to build a list box or data grid.
            RefreshErrors();
        }

        private void View_DownloadCommand(object sender, EventArgs e)
        {
            ProcessResult<string> appDirectoryResult = _fileSystemAccess.GetAppDirectoryPath();
            if (!appDirectoryResult.IsSuccessful)
            {
                _logger.LogError("Could not get application directory path to write logs.");
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
    }
}
