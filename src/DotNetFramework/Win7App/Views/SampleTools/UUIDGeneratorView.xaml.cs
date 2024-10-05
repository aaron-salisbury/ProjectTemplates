using System.Windows.Controls;
using Win7App.Base.Extensions;

namespace Win7App.Views.SampleTools
{
    public partial class UUIDGeneratorView : UserControl
    {
        public UUIDGeneratorView()
        {
            InitializeComponent();
            this.SetDataContext((System.Windows.Application.Current as App)?.Services);
        }
    }
}
