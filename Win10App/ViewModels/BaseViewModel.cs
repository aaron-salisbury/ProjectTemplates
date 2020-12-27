using GalaSoft.MvvmLight;
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
    }
}
