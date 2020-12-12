using FirstFloor.ModernUI.Windows.Controls;
using GalaSoft.MvvmLight;
using Win7Core.Base;
using Win7App.Base.LoadedEvent;
using System.Windows;
using System.Windows.Controls;

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
            get => (ViewModelLocator)Application.Current.FindResource("Locator");
        }

        /// <summary>
        /// Log messages throughout the app.
        /// </summary>
        public AppLogger AppLogger
        {
            get => Locator.ShellWindowViewModel.AppLogger;
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                Set(ref _isBusy, value);
                ProgressBarVisibility = _isBusy ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private Visibility _progressBarVisibility = Visibility.Hidden;
        public Visibility ProgressBarVisibility
        {
            get => _progressBarVisibility;
            set => Set(ref _progressBarVisibility, value);
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
    }
}
