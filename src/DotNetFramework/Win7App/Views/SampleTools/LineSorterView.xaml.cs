using System.Windows;
using System.Windows.Controls;
using Win7App.Base.Extensions;

namespace Win7App.Views.SampleTools
{
    public partial class LineSorterView : UserControl
    {
        public LineSorterView()
        {
            InitializeComponent();
            this.SetDataContext((System.Windows.Application.Current as App)?.Services);
        }

        private async void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            await Application.Current.Dispatcher.InvokeAsync(SelectAllInTextBox);
        }

        private void SelectAllInTextBox()
        {
            tbTextToSort.Focus();
            tbTextToSort.SelectAll();
        }
    }
}
