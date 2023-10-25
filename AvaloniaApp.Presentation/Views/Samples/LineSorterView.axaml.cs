using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApp.Presentation.Base.Extensions;

namespace AvaloniaApp.Presentation.Views
{
    public partial class LineSorterView : UserControl
    {
        public LineSorterView()
        {
            InitializeComponent();
            this.SetDataContext(App.Current?.Services);
        }

        private void SelectAllBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            SortTextTB.Focus();
            SortTextTB.SelectAll();
        }
    }
}
