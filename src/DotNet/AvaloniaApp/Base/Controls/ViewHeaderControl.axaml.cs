using Avalonia;
using Avalonia.Controls;
using System.Reactive.Subjects;

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

    private bool _isBusy;
    public bool IsBusy
    {
        get { return _isBusy; }
        set
        {
            _isBusy = value;
            _progressRingIsActiveSource.OnNext(_isBusy);
        }
    }

    private readonly Subject<bool> _progressRingIsActiveSource = new();

    public ViewHeaderControl()
    {
        InitializeComponent();

        WorkflowProgressRing.Bind(AvaloniaProgressRing.ProgressRing.IsActiveProperty, _progressRingIsActiveSource);
        IsBusy = false;
    }
}