using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Threading.Tasks;
using Win10Core.Base;
using Windows.UI.Xaml;

namespace Win10App.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public ViewModelLocator Locator
        {
            get => ViewModelLocator.Current;
        }

        public AppLogger AppLogger
        {
            get => Locator.ShellViewModel.AppLogger;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                Set(ref _isBusy, value);
                ProgressBarVisibility = _isBusy ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _progressBarVisibility = Visibility.Collapsed;
        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set => Set(ref _progressBarVisibility, value);
        }

        /// <summary>
        /// Tie a RelayCommand to a long running synchronous process that returns a bool indicating whether it completed successfully.
        /// </summary>
        /// <param name="longRunningFunction">Action that performs long running synchronous process.</param>
        /// <param name="taskCommand">The command that when raised triggers this work.</param>
        /// <returns></returns>
        public async Task InitiateProcessAsync(Func<bool> longRunningFunction, RelayCommand taskCommand)
        {
            try
            {
                IsBusy = true;
                await InitiateProcess(longRunningFunction).ConfigureAwait(false);
            }
            finally
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    IsBusy = false;
                    taskCommand.RaiseCanExecuteChanged();
                });
            }
        }

        private Task<bool> InitiateProcess(Func<bool> longRunningFunction)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            Task.Run(() =>
            {
                bool processIsSuccessful = false;

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    processIsSuccessful = longRunningFunction.Invoke();
                });

                tcs.SetResult(processIsSuccessful);
            }).ConfigureAwait(false);

            return tcs.Task;
        }
    }
}
