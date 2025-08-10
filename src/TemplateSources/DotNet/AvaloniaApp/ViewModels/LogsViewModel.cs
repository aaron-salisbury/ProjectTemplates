using AvaloniaApp.Presentation.Desktop.Base.Services;
using AvaloniaApp.Presentation.Desktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RunnethOverStudio.AppToolkit.Presentation.MVVM;
using Serilog.Events;
using Serilog.Sinks.MemorySink;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace AvaloniaApp.Presentation.Desktop.ViewModels;

public partial class LogsViewModel : BaseViewModel
{
    public IAsyncRelayCommand ClearLogsCommand { get; }

    [ObservableProperty]
    private ObservableCollection<ErrorLog> _errorLogs;

    [ObservableProperty]
    private bool _logsExist;

    private readonly ILogSource<LogEvent> _logSource;
    private readonly IAgnosticDispatcher _dispatcher;

    public LogsViewModel(ILogSource<LogEvent> logSource, IAgnosticDispatcher dispatcher)
    {
        _logSource = logSource;
        _dispatcher = dispatcher;

        IEnumerable<LogEvent> logEvents = logSource.GetLogs(0).Result;
        _logsExist = logEvents.Any();
        _errorLogs = new ObservableCollection<ErrorLog>(logEvents
            .Select(le => new ErrorLog() { LogEvent = le, Message = le.RenderMessage() }));

        _errorLogs.CollectionChanged += ErrorLogs_CollectionChanged;
        logSource.LogEmitted += LogSource_LogEmitted;

        ClearLogsCommand = new AsyncRelayCommand(ClearLogsAsync);
    }

    private async Task ClearLogsAsync()
    {
        ErrorLogs.Clear();
        await _logSource.ClearLogs();
    }

    private void ErrorLogs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        LogsExist = ErrorLogs.Count > 0;
    }

    private void LogSource_LogEmitted(object? sender, LogEvent e)
    {
        ErrorLog errorLog = new() { LogEvent = e, Message = e.RenderMessage() };
        _dispatcher.PostBackground(() => ErrorLogs.Add(errorLog));
    }
}
