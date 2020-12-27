using Win10App.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Win10App.Views
{
    public sealed partial class UUIDGeneratorPage : Page
    {
        public UUIDGeneratorPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.UUIDGeneratorViewModel;
        }
    }
}
