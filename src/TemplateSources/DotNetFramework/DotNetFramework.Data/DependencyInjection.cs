using DotNetFrameworkToolkit.Modules.DependencyInjection;
using Microsoft.Practices.Unity.Utility;

namespace DotNetFramework.Data;

public static class DependencyInjection
{
    /// <summary>
    /// Register internal data-tier services.
    /// </summary>
    public static IServiceCollection RegisterInternalDataServices(IServiceCollection services)
    {
        Guard.ArgumentNotNull(services, nameof(services));

        services.AddScoped<IEmbeddedDataAccess, EmbeddedDataAccess>();

        return services;
    }
}
