using Avalonia;
using Avalonia.Controls;
using AvaloniaApp.Presentation.Desktop.Base.Extensions;

namespace AvaloniaApp.Presentation.Desktop.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }
}