using System.Windows;
using System.Windows.Controls;

namespace Win7App.Views.SampleTools
{
    /// <summary>
    /// Interaction logic for LineSorter.xaml
    /// </summary>
    public partial class LineSorter : UserControl
    {
        public LineSorter()
        {
            InitializeComponent();
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
