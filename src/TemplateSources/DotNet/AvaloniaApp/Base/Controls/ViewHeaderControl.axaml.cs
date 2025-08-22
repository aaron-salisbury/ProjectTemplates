using Avalonia;
using Avalonia.Controls;

namespace AvaloniaApp.Base.Controls;

public partial class ViewHeaderControl : UserControl
{
    public static readonly DirectProperty<ViewHeaderControl, string?> FriendlyPageNameProperty =
        AvaloniaProperty.RegisterDirect<ViewHeaderControl, string?>(nameof(FriendlyPageName), o => o.FriendlyPageName, (o, v) => o.FriendlyPageName = v);

    public static readonly DirectProperty<ViewHeaderControl, object?> RibbonContentProperty =
        AvaloniaProperty.RegisterDirect<ViewHeaderControl, object?>(nameof(RibbonContent), o => o.RibbonContent, (o, v) => o.RibbonContent = v);

    //TODO: Need to test that this works. Used to use System.Reactive.Subjects and Subject<bool> for the binding, but lost that reference.
    public static readonly DirectProperty<ViewHeaderControl, bool> IsBusyProperty =
        AvaloniaProperty.RegisterDirect<ViewHeaderControl, bool>(nameof(IsBusy), o => o.IsBusy, (o, v) => o.IsBusy = v);

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

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => SetAndRaise(IsBusyProperty, ref _isBusy, value);
    }

    public ViewHeaderControl()
    {
        InitializeComponent();

        IsBusy = false;
    }
}