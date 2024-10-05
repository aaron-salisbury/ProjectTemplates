using DotNetFramework.Core;
using DotNetFramework.Core.ExtensionHelpers;
using DotNetFramework.Core.Logging;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Threading;
using Win7App.Base;
using Win7App.Base.Services;

namespace Win7App.ViewModels
{
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
        private readonly IAgnosticDispatcher _dispatcher;

        public LogsViewModel(InMemorySinkPNP logSource, ILogger logger, IAgnosticDispatcher dispatcher)
        {
            _logSource = logSource;
            _logger = logger;
            _dispatcher = dispatcher;
            _errorLogs = new List<string>(_logSource.Logs);

            DownloadCommand = new RelayCommand(async (object o) => await DownloadLogAsync(), (object o) => !IsBusy);
        }

        public void WireErrors()
        {
            _logSource.LogEmitted += LogSource_LogEmitted;
        }

        private async Task DownloadLogAsync()
        {
            string appDirectoryPath = IO.GetAppDirectoryPath();
            string logsPath = Path.Combine(appDirectoryPath, "Logs");
            string fileName = $"Logs_{DateTimeExtensions.ToTimeStamp(DateTime.Now)}.txt";

            if (IO.WriteFile(_logger, _errorLogs, fileName, logsPath))
            {
                _logger.LogInformation("Wrote log file '{0}' to '{1}'", fileName, logsPath);

                await Task.Run(() => IO.OpenFile(Path.Combine(logsPath, fileName), _logger));
            }
        }

        private void LogSource_LogEmitted(object sender, LogEmitEventArgs e)
        {
            _dispatcher.PostBackground(() => ErrorLogs = new List<string>(_logSource.Logs));
        }
    }
}
