using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using Win7App.Base.Extensions;

namespace Win7App
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : ModernWindow
    {
        public ShellWindow()
        {
            InitializeComponent();

            AppearanceManager.Current.LoadAppearancesFromSettings(Properties.Settings.Default);
        }
    }
}
