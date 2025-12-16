using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using DotNetFramework.Business.Modules.Sample.DomainServices;
using DotNetFramework.Data;
using DotNetFrameworkToolkit.Modules.DataAccess.FileSystem;
using DotNetFrameworkToolkit.Modules.DependencyInjection;

namespace DotNetFramework.Business
{
    public static class Builder
    {
        /// <summary>
        /// Adds business-tier services.
        /// Dependent on <see cref="ILogger"/>.
        /// </summary>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection BuildBusinessServices(IServiceCollection services)
        {
            // Necessary infrastructure.
            services.AddScoped<IFileSystemAccess, FileSystemAccess>();
            services.AddScoped<IEmbeddedDataAccess, EmbeddedDataAccess>();

            // Internal business domain logic.
            services.AddScoped<FlatUIColorProvider, FlatUIColorProvider>();
            services.AddScoped<LineSorter, LineSorter>();
            services.AddScoped<UUIDGenerator, UUIDGenerator>();

            // Orchestrated public-facing (application) services.
            services.AddScoped<ISampleToolsService, SampleToolsService>();

            return services;
        }
    }
}
