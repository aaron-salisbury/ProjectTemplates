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
                FrameworkElement contentFrame = GetDescendantFromName(this, "ContentFrame");

                if (contentFrame is ModernFrame)
                {
                    ModernFrame frame = contentFrame as ModernFrame;
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
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is FrameworkElement)
                {
                    FrameworkElement frameworkElement = child as FrameworkElement;

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
