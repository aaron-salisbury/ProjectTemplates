using DotNetFrameworkToolkit.Core;
using DotNetFrameworkToolkit.Modules.DependencyInjection;
using DotNetFrameworkToolkit.Modules.FileSystem;
using DotNetFrameworkToolkit.Modules.Logging;
using Microsoft.Practices.Unity.Utility;
using System;
using System.Reflection;
using Win98App.Base.MVP;
using BusinessDI = DotNetFramework.Business.DependencyInjection;
using DataDI = DotNetFramework.Data.DependencyInjection;

namespace Win98App;

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

        services.AddSingleton<Navigator, Navigator>();
        services.AddSingleton<ShellForm, ShellForm>();

        // Presenters.
        foreach (Type assemblyType in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (assemblyType.Name.EndsWith("Presenter") && !assemblyType.Name.Equals("Presenter"))
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
