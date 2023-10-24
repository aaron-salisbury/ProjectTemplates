using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using AvaloniaApp.Presentation.Base.Extensions;
using AvaloniaApp.Presentation.ViewModels;

namespace AvaloniaApp.Presentation.Views
{
    public partial class UUIDGeneratorView : UserControl
    {
        public UUIDGeneratorView()
        {
            InitializeComponent();
            this.SetDataContext(App.Current?.Services);
        }

        private async void CopyBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            if (DataContext is UUIDGeneratorViewModel viewModel && viewModel.UUID != null)
            {
                var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;

                if (clipboard != null)
                {
                    var dataObject = new DataObject();
                    dataObject.Set(DataFormats.Text, viewModel.UUID);

                    await clipboard.SetDataObjectAsync(dataObject);
                }
            }
        }
    }
}
