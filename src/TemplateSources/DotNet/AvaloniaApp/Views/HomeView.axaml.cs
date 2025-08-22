using Avalonia;
using Avalonia.Controls;
using AvaloniaApp.Base.Extensions;

namespace AvaloniaApp.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }
}