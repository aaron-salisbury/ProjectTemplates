using Avalonia.Controls;
using AvaloniaApp.Presentation.Base.Extensions;

namespace AvaloniaApp.Presentation.Views
{
    public partial class UUIDGeneratorView : UserControl
    {
        public UUIDGeneratorView()
        {
            InitializeComponent();
            this.SetDataContext(App.Current?.Services);
        }
    }
}
