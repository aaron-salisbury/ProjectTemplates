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
        public RelayCommand ToolsViewCommand { get; }

        public MainWindowViewModel()
        {
            _currentView = new IntroductionViewModel();

            IntroductionViewCommand = new RelayCommand(() => { CurrentView = App.Current?.Services?.GetService<IntroductionViewModel>(); });
            ToolsViewCommand = new RelayCommand(() => { CurrentView = App.Current?.Services?.GetService<ToolsViewModel>(); });
        }
    }
}
