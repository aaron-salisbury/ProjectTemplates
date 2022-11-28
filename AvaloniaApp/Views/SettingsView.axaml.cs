using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace AvaloniaApp.Views
{
    public partial class SettingsView : UserControl
    {
        public ViewModels.SettingsViewModel? ViewModel => DataContext as ViewModels.SettingsViewModel;

        public SettingsView()
        {
            InitializeComponent();
            DataContext = App.Current?.Services?.GetService<ViewModels.SettingsViewModel>();
        }

        private void OnAppThemeRadioClick(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton appThemeRadioButton && 
                Application.Current != null && 
                Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop &&
                ViewModel != null &&
                ViewModel.AppSettings != null)
            {
                FluentThemeMode mode;

                switch (appThemeRadioButton.Name)
                {
                    case "LightRadioButton":
                        mode = FluentThemeMode.Light;
                        break;

                    case "DarkRadioButton":
                    default:
                        mode = FluentThemeMode.Dark;
                        break;
                }

                ViewModel.AppSettings.ThemeMode = mode.ToString();

                ((MainWindow)desktop.MainWindow).UpdateTheme(mode);
                Dispatcher.UIThread.InvokeAsync(ViewModel.Save, DispatcherPriority.Background);
            }
        }
    }
}
