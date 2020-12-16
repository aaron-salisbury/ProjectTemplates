using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Win7Core.SampleTools;
using System.Threading.Tasks;

namespace Win7App.ViewModels.SampleTools
{
    public class LineSorterViewModel : BaseViewModel
    {
        public LineSorter LineSorter { get; set; }

        public RelayCommand ExecuteTaskCommand { get; }

        public LineSorterViewModel()
        {
            LineSorter = new LineSorter(AppLogger);
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(), () => !IsBusy);
        }

        private async Task InitiateProcessAsync()
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
                // Do long running synchronous work here...
                bool processIsSuccessful = LineSorter.Initiate();

                tcs.SetResult(processIsSuccessful);
            }).ConfigureAwait(false);

            return tcs.Task;
        }
    }
}
