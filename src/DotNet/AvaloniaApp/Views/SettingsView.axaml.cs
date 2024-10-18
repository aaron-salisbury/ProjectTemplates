using Avalonia;
using Avalonia.Controls;
using AvaloniaApp.Presentation.Desktop.Base.Extensions;

namespace AvaloniaApp.Presentation.Desktop.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }
}