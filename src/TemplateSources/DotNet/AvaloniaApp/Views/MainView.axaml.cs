using Avalonia.Controls;
using AvaloniaApp.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AvaloniaApp.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}
