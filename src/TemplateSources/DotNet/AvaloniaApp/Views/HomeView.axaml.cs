using Avalonia.Controls;
using AvaloniaApp.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AvaloniaApp.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}