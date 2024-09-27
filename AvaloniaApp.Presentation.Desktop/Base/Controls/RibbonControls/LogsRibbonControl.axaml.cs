using Avalonia;
using Avalonia.Controls;
using AvaloniaApp.Presentation.Desktop.ViewModels;

namespace AvaloniaApp.Presentation.Desktop.Base.Controls;

public partial class LogsRibbonControl : UserControl
{
    public LogsRibbonControl()
    {
        InitializeComponent();
        this.DataContext = (Application.Current as App)?.Services.GetService(typeof(LogsViewModel));
    }
}
