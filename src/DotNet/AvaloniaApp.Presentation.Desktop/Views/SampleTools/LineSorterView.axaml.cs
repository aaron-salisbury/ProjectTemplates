using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApp.Presentation.Desktop.Base.Extensions;

namespace AvaloniaApp.Presentation.Desktop.Views;

public partial class LineSorterView : UserControl
{
    public LineSorterView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }

    private void SelectAllBtn_Click(object? sender, RoutedEventArgs e)
    {
        SortTextTB.Focus();
        SortTextTB.SelectAll();
    }
}