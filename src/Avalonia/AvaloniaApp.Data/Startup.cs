using DotNet.Data.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace DotNet.Data;

public static class Startup
{
    /// <summary>
    /// Adds the <see cref="IHttpClientFactory"/> and related services to the <see cref="IServiceCollection"/> and configures
    /// a named <see cref="HttpClient"/> used for compression.
    /// Also adds a scoped service of the type <see cref="WebRequester"/> that is a wrapper for web requests.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddWebRequesting(this IServiceCollection services)
    {
        services.AddHttpClient(WebRequester.COMPRESSION_CLIENT_NAME, c => { c.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, gzip"); })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });

        services.AddScoped<WebRequester, WebRequester>();

        return services;
    }

    /// <summary>
    /// Registers the <see cref="ApplicationDbContext"/> as a service.
    /// Also adds a scoped service of the type <see cref="IDataAccess"/> that is a CRUD wrapper for the context and 
    /// includes <see cref="DataInitializer"/> for initializing the database.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>()
            .AddScoped<IDataAccess, ApplicationDataAccess>()
            .AddScoped<DataInitializer, DataInitializer>();

        return services;
    }
}
