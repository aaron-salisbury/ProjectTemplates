using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApp.Views
{
    public partial class IntroductionView : UserControl
    {
        public IntroductionView()
        {
            InitializeComponent();
            DataContext = App.Current?.Services?.GetService<ViewModels.IntroductionViewModel>();
        }
    }
}
