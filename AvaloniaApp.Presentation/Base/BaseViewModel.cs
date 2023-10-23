using AvaloniaApp.Presentation.Base.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;

namespace AvaloniaApp.Presentation.Base
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private bool? _longRunningProcessSuccessful = null;

        public virtual void AddModelEvents() { }

        public virtual void RemoveModelEvents() { }

        /// <summary>
        /// Execute long running process without locking the UI thread.
        /// </summary>
        /// <param name="longRunningFunction">Function to execute. If its process fails, it needs to return false or null.</param>
        internal async Task<T> InitiateLongRunningProcessAsync<T>(Func<T> longRunningFunction, IAgnosticDispatcher dispatcher)
        {
            try
            {
                IsBusy = true;

                return await dispatcher.InvokeOnBackgroundAsync(() =>
                {
                    TaskCompletionSource<T> tcs = new();

                    Task.Run(() =>
                    {
                        T result = longRunningFunction.Invoke();
                        tcs.SetResult(result);
                    }).ConfigureAwait(false);

                    LongRunningProcessSuccessful = (tcs.Task.Result is bool boolResult) ? boolResult : tcs.Task.Result != null;

                    return tcs.Task;
                });
            }
            finally
            {
                IsBusy = false;
                LongRunningProcessSuccessful = null;
            }
        }
    }
}
