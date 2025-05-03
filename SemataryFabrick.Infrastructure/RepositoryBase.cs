using SemataryFabrick.Infrastructure.Implementations.Contexts;
using System.Linq.Expressions;

namespace SemataryFabrick.Infrastructure;

public abstract class RepositoryBase<TEntity>(ApplicationContext context)
    where TEntity : class, new()
{
    protected virtual async Task CreateAsync(TEntity entity) => await context.AddAsync(entity);

    protected void Update(TEntity entity) => context.Update(entity);

    protected void Delete(TEntity entity) => context.Remove(entity);

    protected async Task AddRangeAsync(IEnumerable<TEntity> entities) =>
       context.Set<TEntity>().AddRangeAsync(entities);

    protected virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        context.Set<TEntity>().RemoveRange(entities);
        await Task.CompletedTask;
    }

    protected IQueryable<TEntity> Find(Expression<Func<TEntity, bool>>? filter = null) =>
        filter is null ? context.Set<TEntity>() : context.Set<TEntity>().Where(filter);
}