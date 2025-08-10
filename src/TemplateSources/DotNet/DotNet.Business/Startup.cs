using DotNet.Business.Modules.Sample.ApplicationServices;
using DotNet.Business.Modules.Sample.DomainServices;
using DotNet.Data;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System.Reflection;

namespace DotNet.Business;

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
        services.AddSingleton<IEventSystem, EventSystem>()
            .AddWebRequesting()
            .AddDataAccess();

        // Internal business domain.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddScoped<FlatUIColorPicker, FlatUIColorPicker>()
            .AddScoped<LineSorter, LineSorter>()
            .AddScoped<UUIDGenerator, UUIDGenerator>();

        // Orchestrated public-facing (application) services.
        services.AddScoped<ISampleToolsService, SampleToolsService>();

        return services;
    }
}
