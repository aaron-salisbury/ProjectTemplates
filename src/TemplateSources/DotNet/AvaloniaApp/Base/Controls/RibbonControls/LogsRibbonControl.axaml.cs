using AvaloniaApp.Base.Controls.RibbonControls;
using AvaloniaApp.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AvaloniaApp.Base.Controls;

public partial class LogsRibbonControl : BaseRibbonControl
{
    public LogsRibbonControl()
    {
        InitializeComponent();
        this.DataContext = Ioc.Default.GetService(typeof(LogsViewModel));
    }
}
