using System;
using Win10App.Base.Helpers;
using Win10App.Base.Services;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Win10App
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            //TODO: Log and handle the exception as appropriate to your scenario. Possibly save user/app data. The following is a generic implementation:
            e.Handled = true;
            string message = $"We are sorry, but something just went very wrong. 🙈\n\nError: {e.Message}";
            Windows.UI.Popups.MessageDialog messageDialog = new Windows.UI.Popups.MessageDialog(message);
            messageDialog.ShowAsync().GetResults();
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(ViewModels.IntroductionViewModel), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            DispatcherHelper.Initialize();
            return new ShellPage();
        }
    }
}
