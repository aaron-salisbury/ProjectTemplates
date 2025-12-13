using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using AvaloniaApp.Base.Extensions;
using AvaloniaApp.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace AvaloniaApp.Views;

public partial class UUIDGeneratorView : UserControl
{
    public UUIDGeneratorView()
    {
        InitializeComponent();
        this.SetDataContext(Ioc.Default);
    }

    private async void CopyBtn_Click(object? sender, RoutedEventArgs e)
    {
        if (DataContext is UUIDGeneratorViewModel viewModel && viewModel.UUID != null)
        {
            IClipboard? clipboard = TopLevel.GetTopLevel(this)?.Clipboard;

            if (clipboard != null)
            {
                DataTransfer dataObject = new();
                dataObject.Add(DataTransferItem.Create(DataFormat.Text, viewModel.UUID));

                await clipboard.SetDataAsync(dataObject);
            }
        }
    }
}
