using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using AvaloniaApp.Presentation.Desktop.Base.Extensions;
using AvaloniaApp.Presentation.Desktop.ViewModels;

namespace AvaloniaApp.Presentation.Desktop.Views;

public partial class UUIDGeneratorView : UserControl
{
    public UUIDGeneratorView()
    {
        InitializeComponent();
        this.SetDataContext((Application.Current as App)?.Services);
    }

    private async void CopyBtn_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is UUIDGeneratorViewModel viewModel && viewModel.UUID != null)
        {
            IClipboard? clipboard = TopLevel.GetTopLevel(this)?.Clipboard;

            if (clipboard != null)
            {
                DataObject dataObject = new();
                dataObject.Set(DataFormats.Text, viewModel.UUID);

                await clipboard.SetDataObjectAsync(dataObject);
            }
        }
    }
}
