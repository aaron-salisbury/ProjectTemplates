using Avalonia;
using Avalonia.Controls;
using AvaloniaApp.Base.Extensions;

namespace AvaloniaApp.Views;

public partial class LogsView : UserControl
{
    public LogsView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }
}