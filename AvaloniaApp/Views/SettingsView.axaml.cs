using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApp.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = App.Current?.Services?.GetService<ViewModels.SettingsViewModel>();
        }
    }
}
