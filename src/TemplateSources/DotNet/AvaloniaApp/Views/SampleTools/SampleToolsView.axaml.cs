using Avalonia.Controls;
using AvaloniaApp.Base.Extensions;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AvaloniaApp.Views;

public partial class SampleToolsView : UserControl
{
    public SampleToolsView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }
}