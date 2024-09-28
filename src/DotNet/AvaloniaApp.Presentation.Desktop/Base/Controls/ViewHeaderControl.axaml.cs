using Avalonia;
using Avalonia.Controls;

namespace AvaloniaApp.Presentation.Desktop.Base.Controls;

public partial class ViewHeaderControl : UserControl
{
    public static readonly DirectProperty<ViewHeaderControl, string?> FriendlyPageNameProperty =
        AvaloniaProperty.RegisterDirect<ViewHeaderControl, string?>(nameof(FriendlyPageName), o => o.FriendlyPageName, (o, v) => o.FriendlyPageName = v);

    public static readonly DirectProperty<ViewHeaderControl, object?> RibbonContentProperty =
        AvaloniaProperty.RegisterDirect<ViewHeaderControl, object?>(nameof(RibbonContent), o => o.RibbonContent, (o, v) => o.RibbonContent = v);

    private string? _friendlyPageName;
    public string? FriendlyPageName
    {
        get { return _friendlyPageName; }
        set { SetAndRaise(FriendlyPageNameProperty, ref _friendlyPageName, value); }
    }

    private object? _ribbonContent;
    public object? RibbonContent
    {
        get { return _ribbonContent; }
        set { SetAndRaise(RibbonContentProperty, ref _ribbonContent, value); }
    }

    public ViewHeaderControl()
    {
        InitializeComponent();
    }
}