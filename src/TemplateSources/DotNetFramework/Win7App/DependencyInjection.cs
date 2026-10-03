using DotNetFrameworkToolkit.Core;
using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.FileSystem;
using DotNetFrameworkToolkit.Modules.Logging;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Win7App.Base.Services;
using BusinessDI = DotNetFramework.Business.DependencyInjection;
using DataDI = DotNetFramework.Data.DependencyInjection;

namespace Win7App;

internal static class DependencyInjection
{
    internal static IServiceCollection BuildServiceCollection()
    {
        string appDirectoryPath = GetApplicationDataDirectory();

        IServiceCollection services = new ServiceCollectionPNP();

        InMemorySinkPNP inMemorySink = new();
        services.AddSingleton<ILogger>(new LoggerPNP(LogLevel.Debug, inMemorySink));
        services.AddSingleton(inMemorySink);

        services.AddScoped<IFileSystemAccess, FileSystemAccess>();

        services = BusinessDI.RegisterInternalBusinessServices(services);
        services = DataDI.RegisterInternalDataServices(services);
        services = RegisterInternalPresentationsServices(services);

        return services;
    }

    /// <summary>
    /// Register internal desktop Presentation-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalPresentationsServices(IServiceCollection services)
    {
        Guard.ArgumentNotNull(services, nameof(services));

        services.AddScoped(typeof(IAgnosticDispatcher), typeof(WPFDispatcher));

        // View models.
        foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (assemblyType.Name.EndsWith("ViewModel") && !assemblyType.Name.Equals("BaseViewModel"))
            {
                services.AddScoped(assemblyType);
            }
        }

        return services;
    }

    private static string GetApplicationDataDirectory()
    {
        FileSystemAccess fileSystemAccess = new(new LoggerPNP());
        ProcessResult<string> appDirectoryPathResult = fileSystemAccess.GetAppDirectoryPath();

        if (appDirectoryPathResult.IsSuccessful)
        {
            return appDirectoryPathResult.Value;
        }

        throw new InvalidOperationException("Failed to get or create application data directory.", appDirectoryPathResult.Error);
    }
}
