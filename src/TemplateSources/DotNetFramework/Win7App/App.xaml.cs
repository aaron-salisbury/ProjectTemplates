using DotNetFrameworkToolkit.Modules.DependencyInjection;
using System;
using System.Windows;
using Win7App.ViewModels;

namespace Win7App;

public partial class App : Application
{
    static App()
    {
        IServiceCollection services = DependencyInjection.BuildServiceCollection();
        IServiceProvider provider = services.BuildServiceProvider();
        Ioc.Default.ConfigureServices(provider);
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
        LogsViewModel logsVM = Ioc.Default.GetRequiredService<LogsViewModel>();
        logsVM.WireErrors();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        Ioc.Default?.Dispose();
    }
}
