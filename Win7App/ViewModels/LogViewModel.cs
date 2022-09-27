using GalaSoft.MvvmLight.Command;

namespace Win7App.ViewModels
{
    public class LogViewModel : BaseViewModel
    {
        private readonly RelayCommand _downloadCommand;
        public RelayCommand DownloadCommand
        {
            get { return _downloadCommand; }
        }

        public LogViewModel()
        {
            _downloadCommand = new RelayCommand(() => DownloadLog(), () => !IsBusy);
        }

        private void DownloadLog()
        {
            AppLogger.OpenLog();
        }
    }
}
