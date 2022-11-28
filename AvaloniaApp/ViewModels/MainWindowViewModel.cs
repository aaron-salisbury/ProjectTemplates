using AvaloniaApp.ViewModels.SampleTools;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private object? _currentView;

        [ObservableProperty]
        private string _currentViewFriendly;

        [ObservableProperty]
        private double _backgroundOpacity;

        public RelayCommand IntroductionViewCommand { get; }
        public RelayCommand LogViewCommand { get; }
        public RelayCommand SettingsViewCommand { get; }
        public RelayCommand ToolsViewCommand { get; }

        public static bool IsDebug
        {
#if DEBUG
            get { return true; }
#else
            get { return false; }
#endif
        }

        public MainWindowViewModel()
        {
            _currentView = new IntroductionViewModel();
            _currentViewFriendly = "Welcome";
            _backgroundOpacity = 1.0D;

            IntroductionViewCommand = new RelayCommand(() => { SetCurrentView<IntroductionViewModel>("Welcome"); });
            LogViewCommand = new RelayCommand(() => { SetCurrentView<LogViewModel>(); });
            SettingsViewCommand = new RelayCommand(() => { SetCurrentView<SettingsViewModel>(); });
            ToolsViewCommand = new RelayCommand(() => { SetCurrentView<ToolsViewModel>(); });
        }

        private void SetCurrentView<T>(string? viewFriendlyName = null) where T : ObservableObject
        {
            CurrentView = App.Current?.Services?.GetService<T>();
            CurrentViewFriendly = viewFriendlyName ?? typeof(T).Name.Replace("ViewModel", string.Empty);
        }
    }
}
