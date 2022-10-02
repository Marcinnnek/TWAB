using System.Linq.Expressions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using TWAB.Application.Common.Persistence.Base;

namespace TWAB.Infrastructure.Persistence.Persistence.Base;

public class Repository<TEntity, TContext> : IRepository<TEntity> 
    where TEntity : class, IEntity
    where TContext : DbContext
{
    protected readonly TContext Context;

    public Repository(TContext context)
    {
        Context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default) => await Context.Set<TEntity>().ToListAsync();

    public virtual async Task<TEntity> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default) => await Context.Set<TEntity>().FindAsync(id);

    public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().Add(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> IsExistAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);
        return entity is null ? false : true;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Entry(entity).State = EntityState.Modified;
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Context.Set<TEntity>().FindAsync(id);
        if (entity == null)
            return false;

        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();

        return true;
    }
}