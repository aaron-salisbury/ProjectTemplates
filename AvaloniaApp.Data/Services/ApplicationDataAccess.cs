using AvaloniaApp.Data.Base.Extensions;
using AvaloniaApp.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace AvaloniaApp.Data;

/// <summary>
/// CRUD wrapper for DbContext.
/// </summary>
public class ApplicationDataAccess : IDataAccess
{
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ApplicationDataAccess(ILogger logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public TEntity Create<TEntity>(TEntity entity)
    {
        if (entity != null)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            EntityEntry dbEntry = context.Add(entity);
            SaveAndReload(dbEntry, context);
        }

        return entity;
    }

    public void CreateRange<TEntity>(IEnumerable<TEntity> entities)
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        List<EntityEntry> dbEntries = [];

        foreach (TEntity entity in entities)
        {
            if (entity != null)
            {
                dbEntries.Add(context.Add(entity));
            }
        }

        SaveAndReload(dbEntries, context);
    }

    public TEntity? Read<TEntity>(int id) where TEntity : class
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return context.Set<TEntity>().Find(id);
    }

    public async Task<TEntity?> ReadAsync<TEntity>(int id) where TEntity : class
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().FindAsync(id);
    }

    public IList<TEntity> ReadFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate, params string[] relationships) where TEntity : class
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        IQueryable<TEntity> query = context.Set<TEntity>().Where(predicate);

        if (relationships.Length != 0)
        {
            query = query.IncludeRelationships(new Stack<string>(relationships));
        }

        return [.. query];
    }

    public IList<TEntity> ReadAll<TEntity>(params string[] relationships) where TEntity : class
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        IQueryable<TEntity> query = context.Set<TEntity>();

        if (relationships.Length != 0)
        {
            query = query.IncludeRelationships(new Stack<string>(relationships));
        }

        return [.. query];
    }

    public TEntity Update<TEntity>(TEntity entity)
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (entity != null)
        {
            EntityEntry dbEntry = context.Update(entity);

            SaveAndReload(dbEntry, context);
        }

        return entity;
    }

    public void Delete<TEntity>(TEntity entity)
    {
        if (entity != null)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            EntityEntry dbEntry = context.Remove(entity);

            SaveAndReload(dbEntry, context);
        }
    }

    public int Count<TEntity>() where TEntity : class
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return context.Set<TEntity>().Count();
    }

    public async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    private void SaveAndReload(EntityEntry dbEntry, DbContext context)
    {
        SaveAndReload([dbEntry], context);
    }

    private void SaveAndReload(IEnumerable<EntityEntry> dbEntries, DbContext context)
    {
        try
        {
            context.SaveChanges();

            foreach (EntityEntry dbEntry in dbEntries)
            {
                dbEntry.Reload();
            }
        }
        catch (DbUpdateException e)
        {
            _logger.LogError(e, "Database save failed.");
        }
    }
}
