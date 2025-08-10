using Microsoft.Extensions.DependencyInjection;
using RunnethOverStudio.AppToolkit.Modules.DataAccess;
using System.Net;

namespace DotNet.Data;

public static class Startup
{
    /// <summary>
    /// Adds the <see cref="IHttpClientFactory"/> and related services to the <see cref="IServiceCollection"/> and configures
    /// a named <see cref="HttpClient"/> used for compression.
    /// Also adds a scoped service of the type <see cref="IHttpRequester"/> that is a wrapper for web requests.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddWebRequesting(this IServiceCollection services)
    {
        services.AddHttpClient(HttpRequester.COMPRESSION_CLIENT_NAME, c => { c.DefaultRequestHeaders.Add("Accept-Encoding", "deflate, gzip"); })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });

        services.AddScoped<IHttpRequester, HttpRequester>();

        return services;
    }

    /// <summary>
    /// Adds a singleton service of the type <see cref="ISQLDataAccess"/> that is a CRUD wrapper for the application's data store.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        SqliteDatabaseInitializer.InitializeDatabase();

        services.AddSingleton<ISQLDataAccess>(_ => new DapperSQLiteDataAccess(SqliteDatabaseInitializer.GetDBPath()));

        return services;
    }
}
