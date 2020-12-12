using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
