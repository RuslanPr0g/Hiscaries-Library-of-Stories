using Enterprise.Domain;
using Enterprise.Domain.DataAccess;
using Microsoft.EntityFrameworkCore;

public abstract class BaseWriteRepository<TEntity, TIdentifier, TContext> : IBaseWriteRepository<TEntity, TIdentifier>
    where TEntity : Entity<TIdentifier>
    where TIdentifier : Identity
    where TContext : DbContext
{
    protected abstract TContext Context { get; init; }

    public async Task<TEntity?> GetById(TIdentifier id)
    {
        return await Context.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public async Task<IEnumerable<TEntity>> GetByIds(TIdentifier[] ids)
    {
        return await Context.Set<TEntity>()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetByIds(Guid[] ids)
    {
        return await Context.Set<TEntity>()
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task Add(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRange(params TEntity[] entities)
    {
        await Context.Set<TEntity>().AddRangeAsync(entities);
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public Task<int> SaveChanges()
    {
        return Context.SaveChangesAsync();
    }
}
