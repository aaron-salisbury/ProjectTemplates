using DotNetFramework.Business;
using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.Logging;
using System;
using System.Reflection;
using System.Windows;
using Win7App.Base.Services;
using Win7App.ViewModels;

namespace Win7App
{
    public partial class App : Application
    {
        public IServiceProvider Services { get; set; } = ConfigureServices();

        static App() { }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollectionPNP();

            // Application level infrastructure.
            InMemorySinkPNP inMemorySink = new();
            //InMemorySinkPNP inMemorySink = new(formatter: new TextFormatter("{message}")); //TODO: Try this after changing logs view to use a control with columns.
            ServiceCollectionExtensions.AddSingleton<ILogger>(services, new LoggerPNP(LogLevel.Debug, inMemorySink));
            ServiceCollectionExtensions.AddSingleton(services, inMemorySink); // So the logs view model can subscribe to emit event.

            // Presentation services.
            ServiceCollectionExtensions.AddScoped(services, typeof(IAgnosticDispatcher), typeof(WPFDispatcher));

            // View models.
            foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (assemblyType.Name.EndsWith("ViewModel") && !assemblyType.Name.Equals("BaseViewModel"))
                {
                    ServiceCollectionExtensions.AddScoped(services, assemblyType);
                }
            }

            // Business domain services.
            Builder.BuildBusinessServices(services);

            return services.BuildServiceProvider();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LogsViewModel logsVM = ServiceProviderExtensions.GetRequiredService<LogsViewModel>(Services);
            logsVM.WireErrors();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (Services != null)
            {
                if (Services.GetService(typeof(ILogger)) is IDisposable disposableLogger)
                {
                    disposableLogger.Dispose();
                }
            }
        }
    }
}
