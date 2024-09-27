using Microsoft.EntityFrameworkCore;

namespace AvaloniaApp.Data.Base.Extensions;

internal static class IQueryableExtensions
{
    /// <summary>
    ///     Specifies, through recursion, related entities to include in the query results. The navigation properties to be included are
    ///     specified starting with the type of entity being queried (<typeparamref name="TEntity" />). Further
    ///     navigation properties to be included can be appended, separated by the '.' character.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity being queried.</typeparam>
    /// <param name="sourceQueryable">The source query.</param>
    /// <param name="relationships">Strings of '.' separated navigation property names to be included.</param>
    /// <returns>A new query with the related data included.</returns>
    internal static IQueryable<TEntity> IncludeRelationships<TEntity>(this IQueryable<TEntity> sourceQueryable, Stack<string> relationships) where TEntity : class
    {
        string relationship = relationships.Pop();

        IQueryable<TEntity> newQueryable = sourceQueryable.Include(relationship);

        if (relationships.Count != 0)
        {
            return IncludeRelationships(newQueryable, relationships);
        }
        else
        {
            return newQueryable;
        }
    }
}
