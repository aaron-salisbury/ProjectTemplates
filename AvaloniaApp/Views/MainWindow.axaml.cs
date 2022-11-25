using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using System.Linq;

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
            Border mainMenuBorder = this.FindControl<Border>("MainMenuBorder");

            foreach (ILogical control in mainMenuBorder.GetLogicalDescendants())
            {
                if (control is TextBlock)
                {
                    TextBlock textControl = (TextBlock)control;
                    if (textControl.Classes.Contains("menuTxt"))
                    {
                        textControl.IsVisible = !textControl.IsVisible;
                    }
                }
                else if (control is Button)
                {
                    Button textButton = (Button)control;
                    if (textButton.Classes.Contains("menuBtnOpen"))
                    {
                        textButton.Classes.Remove("menuBtnOpen");
                        textButton.Classes.Add("menuBtnClosed");
                    }
                    else if (textButton.Classes.Contains("menuBtnClosed"))
                    {
                        textButton.Classes.Remove("menuBtnClosed");
                        textButton.Classes.Add("menuBtnOpen");
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
    }
}
