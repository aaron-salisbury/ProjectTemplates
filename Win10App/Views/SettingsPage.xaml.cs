using Win10App.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Win10App.Views
{
    public sealed partial class SettingsPage : Page
    {
        private SettingsViewModel ViewModel
        {
            get => ViewModelLocator.Current.SettingsViewModel;
        }

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.InitializeAsync();
        }
    }
}
