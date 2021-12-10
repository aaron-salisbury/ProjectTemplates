using Microsoft.Extensions.DependencyInjection;
using System;
using Win10App.Base.Helpers;
using Win10App.Base.Services;
using Win10App.ViewModels;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Win10App
{
    public sealed partial class App : Application
    {
        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private readonly Lazy<ActivationService> _activationService;
        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            Services = ConfigureServices();
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        private static IServiceProvider ConfigureServices()
        {
            // https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/ioc

            var services = new ServiceCollection();

            // Viewmodels
            services.AddSingleton<ShellViewModel>();
            services.AddSingleton<FlatUIColorPickerViewModel>();

            return services.BuildServiceProvider();
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
            return new ActivationService(typeof(Views.IntroductionPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            DispatcherHelper.Initialize();
            return new ShellPage();
        }
    }
}
