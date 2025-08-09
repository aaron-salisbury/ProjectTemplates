using CommunityToolkit.Mvvm.ComponentModel;
using Serilog.Events;

namespace AvaloniaApp.Presentation.Desktop.Models;

public partial class ErrorLog : ObservableObject
{
    private LogEvent? _logEvent;
    public LogEvent? LogEvent
    {
        get => _logEvent;
        set => SetProperty(ref _logEvent, value);
    }

    private string? _message;
    public string? Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }
}
