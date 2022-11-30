using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApp.Views
{
    public partial class SettingsView : UserControl
    {
        public ViewModels.SettingsViewModel? ViewModel => DataContext as ViewModels.SettingsViewModel;

        public SettingsView()
        {
            InitializeComponent();
            DataContext = App.Current?.Services?.GetService<ViewModels.SettingsViewModel>();

            this.FindControl<Slider>("BgOpacitySlider").AddHandler(PointerReleasedEvent, OnBgOpacityPointerReleased, RoutingStrategies.Tunnel);
        }

        private void OnAppThemeRadioClick(object sender, RoutedEventArgs e)
        {
            if (Application.Current != null && 
                Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
                ViewModel != null)
            {
                ((MainWindow)desktop.MainWindow).UpdateTheme(ViewModel.IsDarkSelected);
                Dispatcher.UIThread.InvokeAsync(ViewModel.Save, DispatcherPriority.Background);
            }
        }

        private void OnBgOpacityPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            if (ViewModel != null)
            {
                Dispatcher.UIThread.InvokeAsync(ViewModel.Save, DispatcherPriority.Background);
            }
        }
    }
}
