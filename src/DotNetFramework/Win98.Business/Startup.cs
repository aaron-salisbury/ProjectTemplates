using DotNetFramework.Core.DependencyInjection;
using DotNetFramework.Core.Logging;
using Win98.Business.Modules.Sample.ApplicationServices;
using Win98.Business.Modules.Sample.DomainServices;

namespace Win98.Business
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
            services.AddScoped<FlatUIColorPicker, FlatUIColorPicker>()
                .AddScoped<LineSorter, LineSorter>()
                .AddScoped<UUIDGenerator, UUIDGenerator>();

            // Orchestrated public-facing (application) services.
            services.AddScoped<ISampleToolsService, SampleToolsService>();

            return services;
        }
    }
}
