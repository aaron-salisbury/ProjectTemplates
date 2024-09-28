using System.Windows;
using System.Windows.Controls;

namespace Win7App.Base.LoadedEvent
{
    public class LoadedBindings
    {
        public static readonly DependencyProperty LoadedEnabledProperty =
            DependencyProperty.RegisterAttached(
                "LoadedEnabled",
                typeof(bool),
                typeof(LoadedBindings),
                new PropertyMetadata(false, new PropertyChangedCallback(OnLoadedEnabledPropertyChanged)));

        public static bool GetLoadedEnabled(DependencyObject sender)
        {
            return (bool)sender.GetValue(LoadedEnabledProperty);
        }

        public static void SetLoadedEnabled(DependencyObject sender, bool value)
        {
            sender.SetValue(LoadedEnabledProperty, value);
        }

        private static void OnLoadedEnabledPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ContentControl)
            {
                ContentControl contentControl = sender as ContentControl;
                bool newEnabled = (bool)e.NewValue;
                bool oldEnabled = (bool)e.OldValue;

                if (oldEnabled && !newEnabled)
                {
                    contentControl.Loaded -= MyContentControlLoaded;
                }
                else if (!oldEnabled && newEnabled)
                {
                    contentControl.Loaded += MyContentControlLoaded;
                }
            }
        }

        private static void MyContentControlLoaded(object sender, RoutedEventArgs e)
        {
            ILoadedAction loadedAction = GetLoadedAction((ContentControl)sender);

            if (loadedAction != null)
            {
                loadedAction.ContentControlLoaded();
            }
        }

        public static readonly DependencyProperty LoadedActionProperty =
            DependencyProperty.RegisterAttached(
                "LoadedAction",
                typeof(ILoadedAction),
                typeof(LoadedBindings),
                new PropertyMetadata(null));

        public static ILoadedAction GetLoadedAction(DependencyObject sender)
        {
            return (ILoadedAction)sender.GetValue(LoadedActionProperty);
        }

        public static void SetLoadedAction(DependencyObject sender, ILoadedAction value)
        {
            sender.SetValue(LoadedActionProperty, value);
        }
    }
}
