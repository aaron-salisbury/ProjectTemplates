using DotNetFramework.Business;
using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.Logging;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace WinXPApp
{
    static class Program
    {
        internal static IServiceProvider Services = ConfigureServices();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ApplicationExit += Application_ApplicationExit;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ShellForm());
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollectionPNP();

            // Application level infrastructure.
            InMemorySinkPNP inMemorySink = new();
            //InMemorySinkPNP inMemorySink = new(formatter: new TextFormatter("{message}")); //TODO: Try this after changing logs view to use a control with columns.
            services.AddSingleton<ILogger>(new LoggerPNP(LogLevel.Debug, inMemorySink))
                .AddSingleton(inMemorySink); // So the logs presenter can subscribe to emit event.

            // Presenters.
            foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (assemblyType.Name.EndsWith("Presenter") && !assemblyType.Name.Equals("Presenter"))
                {
                    services.AddScoped(assemblyType);
                }
            }

            // Business domain services.
            services.AddBusinessServices();

            return services.BuildServiceProvider();
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (Services != null)
            {
                if (Services.GetService<ILogger>() is IDisposable disposableLogger)
                {
                    disposableLogger.Dispose();
                }
            }
        }
    }
}
