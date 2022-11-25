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

        public RelayCommand IntroductionViewCommand { get; }
        public RelayCommand LogViewCommand { get; }
        public RelayCommand SettingsViewCommand { get; }
        public RelayCommand ToolsViewCommand { get; }

        public MainWindowViewModel()
        {
            _currentView = new IntroductionViewModel();

            IntroductionViewCommand = new RelayCommand(() => { CurrentView = App.Current?.Services?.GetService<IntroductionViewModel>(); });
            LogViewCommand = new RelayCommand(() => { CurrentView = App.Current?.Services?.GetService<LogViewModel>(); });
            SettingsViewCommand = new RelayCommand(() => { CurrentView = App.Current?.Services?.GetService<SettingsViewModel>(); });
            ToolsViewCommand = new RelayCommand(() => { CurrentView = App.Current?.Services?.GetService<ToolsViewModel>(); });
        }
    }
}
