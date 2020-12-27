using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using System.Windows.Media;
using Win7App.Base;

namespace Win7App.Views
{
    /// <summary>
    /// Interaction logic for PopUpWindow.xaml
    /// </summary>
    public partial class PopUpWindow : ModernWindow
    {
        public PopUpWindow()
        {
            InitializeComponent();
            RegisterNavigation();
        }

        // ModernWindows have their own built in navigation, but this should let pop ups bypass that since we wouldn't want main menus, etc.
        private void RegisterNavigation()
        {
            // www.c-sharpcorner.com/UploadFile/3789b7/modern-ui-for-wpf-application-by-example-navigationmessages/

            Messenger.Default.Register<NavigationMessage>(this, p =>
            {
                if (GetDescendantFromName(this, "ContentFrame") is ModernFrame frame)
                {
                    frame.Source = new Uri(p.TargetPage, UriKind.Relative);
                }
            });
        }

        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                if (VisualTreeHelper.GetChild(parent, i) is FrameworkElement frameworkElement)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);

                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }

            return null;
        }
    }
}
