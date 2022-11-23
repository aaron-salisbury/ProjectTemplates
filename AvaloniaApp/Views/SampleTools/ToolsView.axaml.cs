using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApp.Views.SampleTools
{
    public partial class ToolsView : UserControl
    {
        public ToolsView()
        {
            InitializeComponent();
            DataContext = App.Current?.Services?.GetService<ViewModels.SampleTools.ToolsViewModel>();
        }
    }
}
