using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Themes.Fluent;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApp.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = App.Current?.Services?.GetService<ViewModels.SettingsViewModel>();
        }

        private void OnAppThemeRadioClick(object sender, RoutedEventArgs e)
        {
            Cursor = new Cursor(StandardCursorType.Wait);

            if (sender is RadioButton appThemeRadioButton && 
                Application.Current != null && 
                Application.Current.Styles.Count > 1 && 
                Application.Current.Styles[0] is FluentTheme fluentTheme &&
                Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
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

                fluentTheme.Mode = mode;
                ((MainWindow)desktop.MainWindow).UpdateTheme(mode);
            }

            Cursor = new Cursor(StandardCursorType.Arrow);
        }
    }
}
