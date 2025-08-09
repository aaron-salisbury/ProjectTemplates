using System.Windows.Controls;
using Win7App.Base.Extensions;

namespace Win7App.Views
{
    public partial class LogsView : UserControl
    {
        public LogsView()
        {
            InitializeComponent();
            this.SetDataContext((System.Windows.Application.Current as App)?.Services);
        }
    }
}
