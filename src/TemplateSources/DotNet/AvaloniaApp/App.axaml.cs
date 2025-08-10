using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaApp.Presentation.Desktop.Base.Services;
using AvaloniaApp.Presentation.Desktop.Views;
using DotNet.Business;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RunnethOverStudio.AppToolkit.Presentation.MVVM;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MemorySink;
using System;
using System.Reflection;

namespace AvaloniaApp.Presentation.Desktop
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; set; } = null!; // Set during the Startup event to avoid conflicts prior to Initialization completing.

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CT
                BindingPlugins.DataValidators.RemoveAt(0);

                desktop.Startup += Desktop_Startup;
                desktop.Exit += Desktop_Exit;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void Desktop_Startup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
            // Services need to be gathered after app initialization but before
            // the MainWindow is created as that starts the chain of dependency injections.
            Services = ConfigureServices();

            if (sender is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }
        }

        private void Desktop_Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            Serilog.Log.CloseAndFlush();
        }

        private static ServiceProvider ConfigureServices()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.MemorySink(out ILogSource<LogEvent> logSource)
                .CreateLogger();

            ServiceCollection services = new();

            // Application level infrastructure.
            services.AddLogging(configure => configure.AddSerilog(Serilog.Log.Logger))
                .AddSingleton((sp) => { return sp.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(App)); });

            // Presentation services.
            services.AddScoped(typeof(IAgnosticDispatcher), typeof(AvaloniaDispatcher))
                .AddSingleton(logSource);

            // View models.
            foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (typeof(BaseViewModel).IsAssignableFrom(assemblyType))
                {
                    services.AddScoped(assemblyType);
                }
            }

            // Business domain services.
            services.AddBusinessServices();

            return services.BuildServiceProvider();
        }
    }
}
