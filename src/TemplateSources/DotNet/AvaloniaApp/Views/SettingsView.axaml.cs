using Avalonia;
using Avalonia.Controls;
using AvaloniaApp.Base.Extensions;

namespace AvaloniaApp.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }
}