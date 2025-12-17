using DotNetFrameworkToolkit.Modules.DependencyInjection;
using System.Windows.Controls;
using Win7App.Base.Extensions;

namespace Win7App.Views;

public partial class SettingsAppearanceView : UserControl
{
    public SettingsAppearanceView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}
