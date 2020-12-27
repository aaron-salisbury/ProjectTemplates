using Win10App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win10App.Views
{
    public sealed partial class LineSorterPage : Page
    {
        public LineSorterPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.LineSorterViewModel;
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
             GalaSoft.MvvmLight.Threading.DispatcherHelper.CheckBeginInvokeOnUI(SelectAllInTextBox);
        }

        private void SelectAllInTextBox()
        {
            tbTextToSort.Focus(FocusState.Programmatic);
            tbTextToSort.SelectAll();
        }
    }
}
