using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Win7App.Base.Extensions;
using Win7App.ViewModels;

namespace Win7App
{
    public partial class ShellWindow : ModernWindow
    {
        public ShellWindow()
        {
            InitializeComponent();
            this.DataContext = new ShellWindowViewModel();
            AppearanceManager.Current.LoadAppearancesFromSettings(Properties.Settings.Default);
        }
    }
}
