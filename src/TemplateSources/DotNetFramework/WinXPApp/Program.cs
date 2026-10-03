using DotNetFrameworkToolkit.Modules.DependencyInjection;
using System;
using System.Windows.Forms;

namespace WinXPApp;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            IServiceCollection services = DependencyInjection.BuildServiceCollection();
            IServiceProvider provider = services.BuildServiceProvider();
            Ioc.Default.ConfigureServices(provider);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ShellForm());
        }
        finally
        {
            Ioc.Default?.Dispose();
        }
    }
}
