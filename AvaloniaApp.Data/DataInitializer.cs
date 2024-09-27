using AvaloniaApp.Data.Database;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaApp.Data;

public class DataInitializer
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DataInitializer(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync()
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        //TODO: Replace with MigrateAsync() after starting to use migrations.
        await context.Database.EnsureCreatedAsync();
    }
}
