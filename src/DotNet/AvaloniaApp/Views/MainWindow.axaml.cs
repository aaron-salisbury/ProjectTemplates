using Avalonia.Controls;
using AvaloniaApp.Base;
using System.Runtime.InteropServices;

namespace AvaloniaApp.Presentation.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.Title = AppInfo.AppDisplayName;

        SetWindowMainGrid(TitleBarAndContentGrid);
    }

    public static void SetWindowMainGrid(Grid mainGrid)
    {
        RowDefinition titleBarRow = new() { Height = new GridLength(GetTitleBarHeight()) };
        RowDefinition contentRow = new() { Height = GridLength.Star };

        mainGrid.RowDefinitions.Add(titleBarRow);
        mainGrid.RowDefinitions.Add(contentRow);
    }

    public static double GetTitleBarHeight()
    {
        return RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? 0.0D : 30.0D;
    }
}