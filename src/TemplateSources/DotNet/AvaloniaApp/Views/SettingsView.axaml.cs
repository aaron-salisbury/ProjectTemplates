using Avalonia.Controls;
using AvaloniaApp.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AvaloniaApp.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}