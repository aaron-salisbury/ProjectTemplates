using DotNetFramework.Core.DependencyInjection;
using DotNetFramework.Core.Logging;
using System;
using System.Reflection;
using System.Windows.Forms;
using Win98.Business;
using Win98App.Base.Logging;
using Win98App.Base.MVP;
using IServiceProvider = DotNetFramework.Core.DependencyInjection.IServiceProvider;

namespace Win98App
{
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
            Application.Run(_services.GetRequiredService<ShellForm>());
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollectionPNP();

            // Application level infrastructure.
            InMemorySink inMemorySink = new();
            services.AddSingleton<ILogger>(new LoggerPNP(LogLevel.Debug, inMemorySink))
                .AddSingleton(inMemorySink) // So the logs presenter can subscribe to emit event.
                .AddSingleton<Navigator, Navigator>()
                .AddSingleton<ShellForm, ShellForm>();

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
            if (_services != null)
            {
                if (_services.GetService<ILogger>() is IDisposable disposableLogger)
                {
                    disposableLogger.Dispose();
                }
            }
        }
    }
}
