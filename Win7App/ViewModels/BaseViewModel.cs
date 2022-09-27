using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Win7App.Base.LoadedEvent;
using Win7Core.Base;

namespace Win7App.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        /// <summary>
        /// Bind ContentControl load event to this in order to do setup work that shouldn't be in the view model's constructor.
        /// </summary>
        private DelegateLoadedAction _loadAction = null;
        public DelegateLoadedAction LoadAction
        {
            get
            {
                _loadAction = _loadAction ?? new DelegateLoadedAction(OnLoad);
                return _loadAction;
            }
        }

        /// <summary> 
        /// Override to do setup work that shouldn't be in the view model's constructor.
        /// </summary>
        public virtual void OnLoad() { }

        public ViewModelLocator Locator
        {
            get { return (ViewModelLocator)Application.Current.FindResource("Locator"); }
        }

        /// <summary>
        /// Log messages throughout the app.
        /// </summary>
        public AppLogger AppLogger
        {
            get { return Locator.ShellWindowViewModel.AppLogger; }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                Set(ref _isBusy, value);
                ProgressBarVisibility = _isBusy ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private Visibility _progressBarVisibility = Visibility.Hidden;
        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set { Set(ref _progressBarVisibility, value); }
        }

        /// <summary>
        /// Indicate that a process completed successfully.
        /// </summary>
        public static void ShowSuccessDialog()
        {
            ModernDialog modernDialog = new ModernDialog
            {
                Content = new Views.SuccessModal()
            };
            modernDialog.Buttons = new Button[] { modernDialog.OkButton };
            modernDialog.ShowDialog();
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
                bool processIsSuccessful = longRunningFunction.Invoke();
                tcs.SetResult(processIsSuccessful);
            }).ConfigureAwait(false);

            return tcs.Task;
        }
    }
}
