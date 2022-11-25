using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaApp.ViewModels;
using AvaloniaApp.ViewModels.SampleTools;
using AvaloniaApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<IntroductionViewModel>();
            services.AddSingleton<LogViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<ToolsViewModel>();

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
