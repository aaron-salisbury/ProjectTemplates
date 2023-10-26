using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApp.Presentation.Base;
using AvaloniaApp.Presentation.Base.Extensions;
using AvaloniaApp.Presentation.ViewModels;

namespace AvaloniaApp.Presentation.Views
{
    public partial class IntroductionView : UserControl
    {
        public IntroductionView()
        {
            InitializeComponent();
            this.SetDataContext(App.Current?.Services);
        }

        private void LiceseBtn_OnClick(object? sender, RoutedEventArgs a)
        {
            if (DataContext is IntroductionViewModel viewModel)
            {
                Browser.Open(viewModel.LicenseURL);
            }
        }
    }
}
