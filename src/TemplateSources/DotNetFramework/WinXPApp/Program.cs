using DotNetFramework.Business;
using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.Logging;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace WinXPApp;

static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        IServiceCollection services = BuildServiceCollection();
        IServiceProvider provider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);

        Application.ApplicationExit += Application_ApplicationExit;

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new ShellForm());
    }

    private static IServiceCollection BuildServiceCollection()
    {
        IServiceCollection services = new ServiceCollectionPNP();

        // Application level infrastructure.
        InMemorySinkPNP inMemorySink = new();
        //InMemorySinkPNP inMemorySink = new(formatter: new TextFormatter("{message}")); //TODO: Try this after changing logs view to use a control with columns.
        services.AddSingleton<ILogger>(new LoggerPNP(LogLevel.Debug, inMemorySink));
        services.AddSingleton(inMemorySink); // So the logs presenter can subscribe to emit event.

        // Presenters.
        foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (assemblyType.Name.EndsWith("Presenter") && !assemblyType.Name.Equals("Presenter"))
            {
                services.AddScoped(assemblyType);
            }
        }

        // Business domain services.
        Builder.BuildBusinessServices(services);

        return services;
    }

    private static void Application_ApplicationExit(object sender, EventArgs e)
    {
        if (Ioc.Default != null)
        {
            if (Ioc.Default.GetService(typeof(ILogger)) is IDisposable disposableLogger)
            {
                disposableLogger.Dispose();
            }
        }
    }
}
