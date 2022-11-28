using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;
using AvaloniaApp.ViewModels;
using Material.Icons.Avalonia;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AvaloniaApp.Views
{
    public partial class MainWindow : Window
    {
        private Button _selectedNavButton;

        public MainWindow()
        {
            InitializeComponent();

            UpdateTheme(
                string.Equals(
                    App.Current?.Services?.GetService<SettingsViewModel>()?.AppSettings?.ThemeMode, 
                    "Light", 
                    StringComparison.OrdinalIgnoreCase) 
                ? FluentThemeMode.Light 
                : FluentThemeMode.Dark);

            _selectedNavButton = this.FindControl<Button>("HomeBtn");
            SetupNavButtonIndicators();
        }

        public void UpdateTheme(FluentThemeMode mode)
        {
            Cursor = new Cursor(StandardCursorType.Wait);

            // Update fluent theme.
            if (Application.Current != null &&
                Application.Current.Styles.Count > 1 &&
                Application.Current.Styles[0] is FluentTheme fluentTheme)
            {
                fluentTheme.Mode = mode;
            }

            // Update classes.
            string oldMode = "lightTheme";
            string newMode = "darkTheme";

            if (mode == FluentThemeMode.Light)
            {
                oldMode = "darkTheme";
                newMode = "lightTheme";
            }

            foreach (ILogical control in this.GetLogicalDescendants())
            {
                if (control is StyledElement element && element.Classes.Any(c => c.StartsWith(oldMode)))
                {
                    string oldClassName = element.Classes.Where(c => c.StartsWith(oldMode)).First();
                    string newClassName = oldClassName.Replace(oldMode, newMode);
                    element.Classes.Remove(oldClassName);
                    element.Classes.Add(newClassName);
                }
            }

            Cursor = new Cursor(StandardCursorType.Arrow);
        }

        private void SetupNavButtonIndicators()
        {
            foreach (ILogical control in this.FindControl<Border>("MainMenuBorder").GetLogicalDescendants())
            {
                if (control is Button navButton && navButton.Classes.Contains("navBtn"))
                {
                    navButton.Click += OnNavButtonClick;
                }
            }

            OnNavButtonClick(_selectedNavButton, null);
        }

        private void OnNavButtonClick(object? sender, RoutedEventArgs? e)
        {
            if (sender is Button clickedButton && _selectedNavButton != null)
            {
                Border? oldNavBtnBorder = _selectedNavButton.GetLogicalDescendants().Where(ld => ld is Border se && se.Classes.Contains("menuBtnIndicator")).FirstOrDefault() as Border;
                if (oldNavBtnBorder != null)
                {
                    oldNavBtnBorder.Height = 0.0D;
                }

                Border? newNavBtnBorder = clickedButton.GetLogicalDescendants().Where(ld => ld is Border se && se.Classes.Contains("menuBtnIndicator")).FirstOrDefault() as Border;
                if (newNavBtnBorder != null)
                {
                    newNavBtnBorder.Height = 19.0D;
                    _selectedNavButton = clickedButton;
                }
            }
        }

        private void OnHamburgerClick(object sender, RoutedEventArgs e)
        {
            // Animate button icon.
            MaterialIcon hamburgerIcon = this.FindControl<MaterialIcon>("HamburgerIcon");

            Animation animation = new Animation()
            {
                Duration = TimeSpan.FromSeconds(0.2),
                Children =
                {
                    new KeyFrame()
                    {
                        Setters = { new Setter(WidthProperty, hamburgerIcon.Width * 0.66) },
                        KeyTime = TimeSpan.FromSeconds(0.0)
                    }
                }
            };

            animation.RunAsync(hamburgerIcon, null);

            // Update styles of menu, menu buttons, and menu button text.
            Border mainMenuBorder = this.FindControl<Border>("MainMenuBorder");

            foreach (ILogical control in mainMenuBorder.GetLogicalDescendants())
            {
                if (control is TextBlock menuButtonText && menuButtonText.Classes.Contains("menuTxt"))
                {
                    menuButtonText.IsVisible = !menuButtonText.IsVisible;
                }
                else if (control is Button menuButton)
                {
                    if (menuButton.Classes.Contains("menuBtnOpen"))
                    {
                        menuButton.Classes.Remove("menuBtnOpen");
                        menuButton.Classes.Add("menuBtnClosed");
                    }
                    else if (menuButton.Classes.Contains("menuBtnClosed"))
                    {
                        menuButton.Classes.Remove("menuBtnClosed");
                        menuButton.Classes.Add("menuBtnOpen");
                    }
                }
            }

            if (mainMenuBorder.Classes.Contains("menuOpen"))
            {
                mainMenuBorder.Classes.Remove("menuOpen");
                mainMenuBorder.Classes.Add("menuClosed");
            }
            else if (mainMenuBorder.Classes.Contains("menuClosed"))
            {
                mainMenuBorder.Classes.Remove("menuClosed");
                mainMenuBorder.Classes.Add("menuOpen");
            }
        }

        private void OnSettingsClick(object sender, RoutedEventArgs e)
        {
            Animation animation = new Animation()
            {
                Duration = TimeSpan.FromSeconds(0.5),
                Children =
                {
                    new KeyFrame()
                    {
                        KeyTime = TimeSpan.FromSeconds(0.0),
                        Setters = { new Setter(Avalonia.Media.RotateTransform.AngleProperty, -25d) }
                    },
                     new KeyFrame()
                    {
                        KeyTime = TimeSpan.FromSeconds(0.20),
                        Setters = { new Setter(Avalonia.Media.RotateTransform.AngleProperty, 0d) }
                    },
                    new KeyFrame()
                    {
                        KeyTime = TimeSpan.FromSeconds(0.25),
                        Setters = { new Setter(Avalonia.Media.RotateTransform.AngleProperty, -180d) }
                    }
                }
            };

            animation.RunAsync(this.FindControl<MaterialIcon>("SettingsIcon"), null);
        }
    }
}
