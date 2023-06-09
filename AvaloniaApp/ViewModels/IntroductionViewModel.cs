using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApp.ViewModels
{
    public partial class IntroductionViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _appDisplayName = "Sample App";

        [ObservableProperty]
        private string _aboutDescription = "This is a sample application generated from Aaron Salisbury's Avalonia App Solution Template.";
    }
}
