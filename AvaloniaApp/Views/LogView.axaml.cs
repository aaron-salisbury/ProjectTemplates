using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApp.Views
{
    public partial class LogView : UserControl
    {
        public LogView()
        {
            InitializeComponent();
            DataContext = App.Current?.Services?.GetService<ViewModels.LogViewModel>();
        }
    }
}
