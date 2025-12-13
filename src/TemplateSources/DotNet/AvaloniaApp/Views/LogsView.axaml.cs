using Avalonia.Controls;
using AvaloniaApp.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AvaloniaApp.Views;

public partial class LogsView : UserControl
{
    public LogsView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}