using AvaloniaApp.Presentation.Base;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaApp.Presentation.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    string _greeting = "Welcome to Avalonia!";
}
