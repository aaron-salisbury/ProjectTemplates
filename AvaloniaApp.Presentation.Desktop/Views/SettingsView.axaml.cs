using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApp.Presentation.Desktop.Base;
using AvaloniaApp.Presentation.Desktop.Base.Extensions;
using AvaloniaApp.Presentation.Desktop.ViewModels;

namespace AvaloniaApp.Presentation.Desktop.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }

    private void PrivacyBtn_Click(object? sender, RoutedEventArgs a)
    {
        if (DataContext is SettingsViewModel viewModel)
        {
            Browser.Open(viewModel.PrivacyURL);
        }
    }

    private void IssuesBtn_Click(object? sender, RoutedEventArgs a)
    {
        if (DataContext is SettingsViewModel viewModel)
        {
            Browser.Open(viewModel.IssuesURL);
        }
    }
}