using Win10App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win10App.Views
{
    public sealed partial class LineSorterPage : Page
    {
        public LineSorterViewModel ViewModel { get; } = new LineSorterViewModel();

        public LineSorterPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            Base.Helpers.DispatcherHelper.CheckBeginInvokeOnUI(SelectAllInTextBox);
        }

        private void SelectAllInTextBox()
        {
            tbTextToSort.Focus(FocusState.Programmatic);
            tbTextToSort.SelectAll();
        }
    }
}
