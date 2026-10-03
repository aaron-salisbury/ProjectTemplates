using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using DotNetFramework.Business.Modules.Sample.DomainServices;
using DotNetFrameworkToolkit.Modules.DependencyInjection;
using Microsoft.Practices.Unity.Utility;

namespace DotNetFramework.Business;

public static class DependencyInjection
{
    /// <summary>
    /// Register internal business-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalBusinessServices(IServiceCollection services)
    {
        Guard.ArgumentNotNull(services, nameof(services));

        // Internal business domain logic.
        services.AddScoped<FlatUIColorProvider, FlatUIColorProvider>();
        services.AddScoped<LineSorter, LineSorter>();
        services.AddScoped<UUIDGenerator, UUIDGenerator>();

        // Orchestrated public-facing (application) services.
        services.AddScoped<ISampleToolsService, SampleToolsService>();

        return services;
    }
}
