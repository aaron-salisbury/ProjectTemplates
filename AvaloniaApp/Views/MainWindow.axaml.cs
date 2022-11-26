using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Styling;
using Material.Icons.Avalonia;
using System;

namespace AvaloniaApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnHamburgerClick(object sender, RoutedEventArgs e)
        {
            // Animate button icon.
            MaterialIcon hamburgerIcon = this.FindControl<MaterialIcon>("HamburgerIcon");

            var animation = new Avalonia.Animation.Animation()
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
            MaterialIcon settingsIcon = this.FindControl<MaterialIcon>("SettingsIcon");

            var animation = new Avalonia.Animation.Animation()
            {
                Duration = TimeSpan.FromSeconds(0.4),
                Children =
                {
                    new KeyFrame()
                    {
                        Setters = { new Setter(Avalonia.Media.RotateTransform.AngleProperty, -180d) },
                        KeyTime = TimeSpan.FromSeconds(0.0)
                    }
                }
            };

            animation.RunAsync(settingsIcon, null);
        }
    }
}
