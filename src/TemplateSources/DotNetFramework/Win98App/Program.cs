using DotNetFramework.Business;
using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.Logging;
using System;
using System.Reflection;
using System.Windows.Forms;
using Win98App.Base.MVP;

namespace Win98App;

static class Program
{
    private static IServiceProvider _services = ConfigureServices();

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        Application.ApplicationExit += Application_ApplicationExit;

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(ServiceProviderExtensions.GetRequiredService<ShellForm>(_services));
    }

    private static IServiceProvider ConfigureServices()
    {
        IServiceCollection services = new ServiceCollectionPNP();

        // Application level infrastructure.
        InMemorySinkPNP inMemorySink = new();
        //InMemorySinkPNP inMemorySink = new(formatter: new TextFormatter("{message}")); //TODO: Try this after changing logs view to use a control with columns.
        ServiceCollectionExtensions.AddSingleton<ILogger>(services, new LoggerPNP(LogLevel.Debug, inMemorySink));
        ServiceCollectionExtensions.AddSingleton(services, inMemorySink);
        ServiceCollectionExtensions.AddSingleton<Navigator, Navigator>(services);
        ServiceCollectionExtensions.AddSingleton<ShellForm, ShellForm>(services);

        // Presenters.
        foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (assemblyType.Name.EndsWith("Presenter") && !assemblyType.Name.Equals("Presenter"))
            {
                ServiceCollectionExtensions.AddScoped(services, assemblyType);
            }
        }

        // Business domain services.
        Startup.AddBusinessServices(services);

        return services.BuildServiceProvider();
    }

    private static void Application_ApplicationExit(object sender, EventArgs e)
    {
        if (_services != null)
        {
            if (_services.GetService(typeof(ILogger)) is IDisposable disposableLogger)
            {
                disposableLogger.Dispose();
            }
        }
    }
}
