using DotNetFramework.Core;
using DotNetFramework.Core.Extensions;
using DotNetFramework.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Win98App.Base.Logging;
using Win98App.Base.MVP;
using Win98App.Views;
using static System.Windows.Forms.Control;

namespace Win98App.Presenters
{
    internal class LogsPresenter : Presenter
    {
        private readonly InMemorySink _logSource;
        private readonly ILogger _logger;
        private List<string> _errorLogs;
        private LogsView _view;

        public LogsPresenter(Navigator navigator, InMemorySink logSource, ILogger logger) : base(navigator)
        {
            _logSource = logSource;
            _logger = logger;
        }

        internal override void Display(Control view, ControlCollection window)
        {
            _view = (LogsView)view;

            _view.DownloadCommand += View_DownloadCommand;

            RefreshErrors();
            _logSource.LogEmitted += LogSource_LogEmitted; // So the logs view refreshes with new errors while the logs view is in focus.

            window.Clear();
            window.Add(_view);
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
            RefreshErrors();
        }

        private void View_DownloadCommand(object sender, EventArgs e)
        {
            string appDirectoryPath = IO.GetAppDirectoryPath();
            string logsPath = Path.Combine(appDirectoryPath, "Logs");
            string fileName = $"Logs_{DateTime.Now.ToTimeStamp()}.txt";

            if (IO.WriteFile(_logger, _errorLogs, fileName, logsPath))
            {
                _logger.LogInformation("Wrote log file '{0}' to '{1}'", fileName, logsPath);

                IO.OpenFile(Path.Combine(logsPath, fileName), _logger);
            }
        }
    }
}
