using Avalonia;
using AvaloniaApp.Base.Controls.RibbonControls;
using AvaloniaApp.ViewModels;

namespace AvaloniaApp.Base.Controls;

public partial class LogsRibbonControl : BaseRibbonControl
{
    public LogsRibbonControl()
    {
        InitializeComponent();
        this.DataContext = (Application.Current as App)?.Services.GetService(typeof(LogsViewModel));
    }
}
