using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApp.ViewModels;
using AvaloniaApp.ViewModels.SampleTools;
using AvaloniaApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AvaloniaApp
{
    public partial class App : Application
    {
        public new static App? Current => Application.Current as App;

        public IServiceProvider? Services { get; set; }

        public override void Initialize()
        {
            Services = ConfigureServices();
            AvaloniaXamlLoader.Load(this);
        }

        private static IServiceProvider ConfigureServices()
        {
            // https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/ioc
            ServiceCollection services = new ServiceCollection();

            // This app requires the naming convention that views end in "View" (Base.ViewLocator.cs),
            // ViewModels end in "ViewModel", and that nothing else ends in either.
            foreach (Type appType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (appType.Name.EndsWith("ViewModel"))
                {
                    services.AddSingleton(appType);
                }
            }

            return services.BuildServiceProvider();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = Services?.GetService<MainWindowViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
