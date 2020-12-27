using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Threading.Tasks;
using Win10Core.SampleTools;

namespace Win10App.ViewModels
{
    public class LineSorterViewModel : BaseViewModel
    {
        public LineSorter LineSorter { get; set; }

        public RelayCommand ExecuteTaskCommand { get; }

        public LineSorterViewModel()
        {
            LineSorter = new LineSorter(AppLogger);
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcess(), () => !IsBusy);
        }

        private async Task InitiateProcess()
        {

            try
            {
                IsBusy = true;
                await ExportDataAsync().ConfigureAwait(false);
            }
            finally
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    IsBusy = false;
                    ExecuteTaskCommand.RaiseCanExecuteChanged();
                });
            }
        }

        private Task<bool> ExportDataAsync()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            Task.Run(() =>
            {
                bool processIsSuccessful = false;

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    processIsSuccessful = LineSorter.Initiate();
                });

                tcs.SetResult(processIsSuccessful);
            }).ConfigureAwait(false);

            return tcs.Task;
        }
    }
}
