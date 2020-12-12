using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Win7Core.SampleTools;
using System.Threading.Tasks;
using System.Windows;

namespace Win7App.ViewModels.SampleTools
{
    public class UUIDGeneratorViewModel : BaseViewModel
    {
        public UUIDGenerator UUIDGenerator { get; set; }

        public RelayCommand ExecuteTaskCommand { get; }

        public RelayCommand CopyUUIDCommand { get; }

        public UUIDGeneratorViewModel()
        {
            UUIDGenerator = new UUIDGenerator();
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(), () => !IsBusy);
            CopyUUIDCommand = new RelayCommand(() => Clipboard.SetText(UUIDGenerator.UUID ?? string.Empty));
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
                bool processIsSuccessful = UUIDGenerator.Initiate(AppLogger.Logger);

                tcs.SetResult(processIsSuccessful);
            }).ConfigureAwait(false);

            return tcs.Task;
        }
    }
}
