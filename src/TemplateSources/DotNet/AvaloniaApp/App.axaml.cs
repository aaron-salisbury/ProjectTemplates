using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaApp.Base.Services;
using AvaloniaApp.ViewModels;
using AvaloniaApp.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using DotNet.Business;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RunnethOverStudio.AppToolkit.Presentation.MVVM;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MemorySink;
using System;
using System.Reflection;

namespace AvaloniaApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Startup += OnDesktopStartup;
            desktop.Exit += OnDesktopExit;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnDesktopStartup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
        // Services need to be gathered after app initialization but before
        // the MainWindow is created as that starts the chain of dependency injections.

        IServiceCollection services = BuildServiceCollection();
        IServiceProvider provider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);

        if (sender is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }
    }

    private void OnDesktopExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        Serilog.Log.CloseAndFlush();

        if (sender is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Startup -= OnDesktopStartup;
        }
    }

    private static IServiceCollection BuildServiceCollection()
    {
        Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.MemorySink(out ILogSource<LogEvent> logSource)
            .CreateLogger();

        IServiceCollection services = new ServiceCollection();

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

        return services;
    }
}
