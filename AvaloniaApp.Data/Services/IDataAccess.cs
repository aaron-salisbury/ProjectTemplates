using System.Linq.Expressions;

namespace AvaloniaApp.Data;

public interface IDataAccess
{
    TEntity Create<TEntity>(TEntity entity);
    void CreateRange<TEntity>(IEnumerable<TEntity> entities);
    TEntity? Read<TEntity>(int id) where TEntity : class;
    Task<TEntity?> ReadAsync<TEntity>(int id) where TEntity : class;
    IList<TEntity> ReadFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate, params string[] relationships) where TEntity : class;
    IList<TEntity> ReadAll<TEntity>(params string[] relationships) where TEntity : class;
    int Count<TEntity>() where TEntity : class;
    Task<int> CountAsync<TEntity>() where TEntity : class;
    TEntity Update<TEntity>(TEntity entity);
    void Delete<TEntity>(TEntity entity);
}
