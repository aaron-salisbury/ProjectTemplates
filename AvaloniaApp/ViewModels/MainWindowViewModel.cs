using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private object _currentView;

        public MainWindowViewModel()
        {
            _currentView = new IntroductionViewModel();
        }
    }
}
