using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApp.Presentation.Desktop.Base;
using AvaloniaApp.Presentation.Desktop.Base.Extensions;
using AvaloniaApp.Presentation.Desktop.ViewModels;

namespace AvaloniaApp.Presentation.Desktop.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }

    private void LicenseBtn_Click(object? sender, RoutedEventArgs a)
    {
        if (DataContext is HomeViewModel viewModel)
        {
            Browser.Open(viewModel.LicenseURL);
        }
    }
}