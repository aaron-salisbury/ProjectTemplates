using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApp.Base.Extensions;

namespace AvaloniaApp.Views;

public partial class MainView : UserControl
{
    private const double SETTINGS_TAB_BOTTOM_MARGIN = 105.0D;

    TabItem? _settingsTabItem = null;
    private double _settingsMarginDif = 0.0D;
    private double _titleBarHeight = MainWindow.GetTitleBarHeight();

    public MainView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);

        this.SizeChanged += MainView_OnSizeChanged;
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        this.SizeChanged -= MainView_OnSizeChanged;
    }

    private void MainView_OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        if (_settingsTabItem == null)
        {
            _settingsTabItem = this.FindControl<TabItem>("SettingsTabItem");
            _settingsMarginDif = (_settingsTabItem?.Bounds.Top ?? 0.0D) + _titleBarHeight + SETTINGS_TAB_BOTTOM_MARGIN - (_settingsTabItem?.Bounds.Height ?? 0.0D);
        }

        if (_settingsTabItem != null)
        {
            double marginTop = this.Bounds.Bottom - _settingsMarginDif;
            marginTop = marginTop >= 0.0D ? marginTop : 0.0D;

            _settingsTabItem.Margin = new Thickness(0, marginTop, 0, 0);
        }
    }
}
