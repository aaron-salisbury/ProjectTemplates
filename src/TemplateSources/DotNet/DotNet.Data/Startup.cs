using Microsoft.Extensions.DependencyInjection;
using RunnethOverStudio.AppToolkit.Modules.DataAccess;
using System.Net;
using System.Net.Http;

namespace DotNet.Data;

public static class Startup
{
    /// <summary>
    /// Adds application file-system, database, and web data access services to the <see cref="IServiceCollection"/>. 
    /// Services include <see cref="IFileSystemAccess"/>, <see cref="ISQLDataAccess"/>, and <see cref="IHttpRequester"/>.
    /// Also adds the <see cref="IHttpClientFactory"/> and configures a named <see cref="HttpClient"/> used for compression.
    /// </summary>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        services.AddScoped<IFileSystemAccess, FileSystemAccess>()
            .AddDatabaseAccess()
            .AddWebAccess();

        return services;
    }

    private static IServiceCollection AddDatabaseAccess(this IServiceCollection services)
    {
        services.AddSingleton<IDatabaseInitializer, SqliteDatabaseInitializer>()
            .AddSingleton<ISQLDataAccess, DapperSQLiteDataAccess>();

        return services;
    }

    private static IServiceCollection AddWebAccess(this IServiceCollection services)
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
}
