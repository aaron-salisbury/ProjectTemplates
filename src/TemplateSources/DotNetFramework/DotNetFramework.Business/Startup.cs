using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using DotNetFramework.Business.Modules.Sample.DomainServices;
using DotNetFramework.Data;
using DotNetFrameworkToolkit.Modules.DataAccess.FileSystem;
using DotNetFrameworkToolkit.Modules.DependencyInjection;

namespace DotNetFramework.Business
{
    public static class Startup
    {
        /// <summary>
        /// Adds business-tier services.
        /// Dependent on <see cref="ILogger"/>.
        /// </summary>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddBusinessServices(IServiceCollection services)
        {
            // Necessary infrastructure.
            ServiceCollectionExtensions.AddScoped<IFileSystemAccess, FileSystemAccess>(services);
            ServiceCollectionExtensions.AddScoped<IEmbeddedDataAccess, EmbeddedDataAccess>(services);

            // Internal business domain logic.
            ServiceCollectionExtensions.AddScoped<FlatUIColorProvider, FlatUIColorProvider>(services);
            ServiceCollectionExtensions.AddScoped<LineSorter, LineSorter>(services);
            ServiceCollectionExtensions.AddScoped<UUIDGenerator, UUIDGenerator>(services);

            // Orchestrated public-facing (application) services.
            ServiceCollectionExtensions.AddScoped<ISampleToolsService, SampleToolsService>(services);

            return services;
        }
    }
}
