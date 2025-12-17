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
        static App()
        {
            IServiceCollection services = BuildServiceCollection();
            IServiceProvider provider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(provider);
        }

        private static IServiceCollection BuildServiceCollection()
        {
            IServiceCollection services = new ServiceCollectionPNP();

            // Application level infrastructure.
            InMemorySinkPNP inMemorySink = new();
            //InMemorySinkPNP inMemorySink = new(formatter: new TextFormatter("{message}")); //TODO: Try this after changing logs view to use a control with columns.
            services.AddSingleton<ILogger>(new LoggerPNP(LogLevel.Debug, inMemorySink));
            services.AddSingleton(inMemorySink); // So the logs view model can subscribe to emit event.

            // Presentation services.
            services.AddScoped(typeof(IAgnosticDispatcher), typeof(WPFDispatcher));

            // View models.
            foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (assemblyType.Name.EndsWith("ViewModel") && !assemblyType.Name.Equals("BaseViewModel"))
                {
                    services.AddScoped(assemblyType);
                }
            }

            // Business domain services.
            Builder.BuildBusinessServices(services);

            return services;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            LogsViewModel logsVM = ServiceProviderExtensions.GetRequiredService<LogsViewModel>(Ioc.Default);
            logsVM.WireErrors();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
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
}
