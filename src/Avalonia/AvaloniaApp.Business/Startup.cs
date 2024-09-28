using DotNet.Business.Modules.Sample.ApplicationServices;
using DotNet.Business.Modules.Sample.DomainServices;
using DotNet.Business.Modules.UserAccess.ApplicationServices;
using DotNet.Business.Modules.UserAccess.DomainServices;
using DotNet.Data;
using FluentValidation;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace DotNet.Business;

public static class Startup
{
    /// <summary>
    /// Adds business-tier services, including <see cref="DomainInitializer"/> for safely starting & shutting down the business services.
    /// Dependent on <see cref="ILogger"/>, <see cref="ISessionValueProvider"/>, and everything that implements <see cref="IConsumer"/>.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        // Necessary infrastructure.
        services.AddWebRequesting()
            .AddDataAccess()
            .AddScoped<SessionValueResolver, SessionValueResolver>()
            .AddMessaging();

        // Internal business domain logic.
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddScoped<FlatUIColorPicker, FlatUIColorPicker>()
            .AddScoped<LineSorter, LineSorter>()
            .AddScoped<UUIDGenerator, UUIDGenerator>();

        // Orchestrated public-facing (application) services.
        services.AddScoped<IUserAccessService, UserAccessService>()
            .AddScoped<ISampleToolsService, SampleToolsService>()
            .AddScoped<DomainInitializer, DomainInitializer>();

        return services;
    }

    private static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        Type[] consumers = services
            .Where(s => typeof(IConsumer).IsAssignableFrom(s.ServiceType))
            .Select(s => s.ServiceType)
            .ToArray();

        services.AddMassTransit(c =>
        {
            c.AddConsumers(consumers);
            c.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
        });

        return services;
    }
}
