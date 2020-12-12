using GalaSoft.MvvmLight.Command;

namespace Win7App.ViewModels
{
    public class LogViewModel : BaseViewModel
    {
        public RelayCommand DownloadCommand { get; }

        public LogViewModel()
        {
            DownloadCommand = new RelayCommand(() => DownloadLog(), () => !IsBusy);
        }

        private void DownloadLog()
        {
            AppLogger.OpenLog();
        }
    }
}
