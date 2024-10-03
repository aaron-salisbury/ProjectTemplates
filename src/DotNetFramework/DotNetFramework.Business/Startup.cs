using DotNetFramework.Business.Modules.Sample.ApplicationServices;
using DotNetFramework.Business.Modules.Sample.DomainServices;
using DotNetFramework.Core.DependencyInjection;
using DotNetFramework.Core.Logging;

namespace DotNetFramework.Business
{
    public static class Startup
    {
        /// <summary>
        /// Adds business-tier services.
        /// Dependent on <see cref="ILogger"/>.
        /// </summary>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Necessary infrastructure.

            // Internal business domain logic.
            services.AddScoped<FlatUIColorProvider, FlatUIColorProvider>()
                .AddScoped<LineSorter, LineSorter>()
                .AddScoped<UUIDGenerator, UUIDGenerator>();

            // Orchestrated public-facing (application) services.
            services.AddScoped<ISampleToolsService, SampleToolsService>();

            return services;
        }
    }
}
