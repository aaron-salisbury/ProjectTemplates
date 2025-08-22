using Avalonia;
using Avalonia.Controls;
using AvaloniaApp.Base.Extensions;

namespace AvaloniaApp.Views;

public partial class SampleToolsView : UserControl
{
    public SampleToolsView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }
}