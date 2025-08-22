using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApp.Base.Extensions;

namespace AvaloniaApp.Views;

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