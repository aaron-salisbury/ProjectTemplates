using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using System.Threading.Tasks;
using Win10App.Base.Helpers;
using Win10Core.SampleTools;
using Windows.ApplicationModel.DataTransfer;

namespace Win10App.ViewModels
{
    public class UUIDGeneratorViewModel : BaseViewModel
    {
        public UUIDGenerator UUIDGenerator { get; set; }

        public RelayCommand ExecuteTaskCommand { get; }

        public RelayCommand CopyUUIDCommand { get; }

        public UUIDGeneratorViewModel()
        {
            UUIDGenerator = new UUIDGenerator(AppLogger);
            ExecuteTaskCommand = new RelayCommand(async () => await InitiateProcessAsync(), () => !IsBusy);
            CopyUUIDCommand = new RelayCommand(() => PlatformShim.CopyToClipboard(UUIDGenerator.UUID));
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
                bool processIsSuccessful = false;

                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    processIsSuccessful = UUIDGenerator.Initiate();
                });

                tcs.SetResult(processIsSuccessful);
            }).ConfigureAwait(false);

            return tcs.Task;
        }
    }
}
