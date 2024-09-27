using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaApp.Business;
using AvaloniaApp.Presentation.Desktop.Base;
using AvaloniaApp.Presentation.Desktop.Base.Services;
using AvaloniaApp.Presentation.Desktop.Views;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MemorySink;
using System;
using System.Reflection;

namespace AvaloniaApp.Presentation.Desktop
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; set; } = ConfigureServices();

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

                DesktopStartup(desktop);

                //TODO: Login? Configure single user?

                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private static IServiceProvider ConfigureServices()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.MemorySink(out ILogSource<LogEvent> logSource)
                .CreateLogger();

            ServiceCollection services = new();

            // Application level infrastructure.
            services.AddLogging(configure => configure.AddSerilog(Serilog.Log.Logger))
                .AddSingleton((sp) => { return sp.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(App)); })
                .AddSingleton<ISessionValueProvider>(new Session());

            // Presentation services.
            services.AddScoped(typeof(IAgnosticDispatcher), typeof(AvaloniaDispatcher))
                .AddSingleton(logSource);

            // View models and MassTransit consumers.
            foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (typeof(IConsumer).IsAssignableFrom(assemblyType))
                {
                    // Consumers have to be singletons if their Consume method uses instance values.
                    services.AddSingleton(assemblyType);
                }
                else if (assemblyType.Name.EndsWith("ViewModel") && !assemblyType.Name.Equals("BaseViewModel"))
                {
                    services.AddScoped(assemblyType);
                }
            }

            // Business domain services.
            services.AddBusinessServices();

            return services.BuildServiceProvider();
        }

        private void DesktopStartup(IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += Desktop_Exit;

            Services.GetRequiredService<DomainInitializer>().StartAsync().Wait();
        }

        private void Desktop_Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            Services.GetRequiredService<DomainInitializer>().Shutdown();

            Serilog.Log.CloseAndFlush();
        }
    }
}