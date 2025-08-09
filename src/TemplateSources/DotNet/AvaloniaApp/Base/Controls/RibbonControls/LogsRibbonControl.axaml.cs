using Avalonia;
using AvaloniaApp.Base.Controls.RibbonControls;
using AvaloniaApp.Presentation.Desktop.ViewModels;

namespace AvaloniaApp.Presentation.Desktop.Base.Controls;

public partial class LogsRibbonControl : BaseRibbonControl
{
    public LogsRibbonControl()
    {
        InitializeComponent();
        this.DataContext = (Application.Current as App)?.Services.GetService(typeof(LogsViewModel));
    }
}
