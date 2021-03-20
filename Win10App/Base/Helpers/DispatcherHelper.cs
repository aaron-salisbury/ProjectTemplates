using System;
using System.Text;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Win10App.Base.Helpers
{
    // Based on https://github.com/lbugnion/mvvmlight/blob/master/GalaSoft.MvvmLight/GalaSoft.MvvmLight.Platform%20%28NET45%29/Threading/DispatcherHelper.cs

    public static class DispatcherHelper
    {
        public static CoreDispatcher UIDispatcher { get; private set; }

        /// <summary>
        /// Executes an action on the UI thread. If this method is called
        /// from the UI thread, the action is executed immendiately. If the
        /// method is called from another thread, the action will be enqueued
        /// on the UI thread's dispatcher and executed asynchronously.
        /// </summary>
        public static void CheckBeginInvokeOnUI(Action action)
        {
            if (action == null)
            {
                return;
            }

            CheckDispatcher();

            if (UIDispatcher.HasThreadAccess)
            {
                action();
            }
            else
            {
                _ = UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
            }
        }

        private static void CheckDispatcher()
        {
            if (UIDispatcher == null)
            {
                var error = new StringBuilder("The DispatcherHelper is not initialized.");
                error.AppendLine();

                error.Append("Call DispatcherHelper.Initialize() at the end of App.OnLaunched.");

                throw new InvalidOperationException(error.ToString());
            }
        }

        /// <summary>
        /// Invokes an action asynchronously on the UI thread.
        /// </summary>
        public static IAsyncAction RunAsync(Action action)
        {
            CheckDispatcher();

            return UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
        }

        /// <summary>
        /// This method should be called once on the UI thread to ensure that
        /// the UIDispatcher property is initialized.
        /// </summary>
        public static void Initialize()
        {
            if (UIDispatcher != null)
            {
                return;
            }

            UIDispatcher = Window.Current.Dispatcher;
        }

        /// <summary>
        /// Resets the class by deleting the <see cref="UIDispatcher"/>
        /// </summary>
        public static void Reset()
        {
            UIDispatcher = null;
        }
    }
}
