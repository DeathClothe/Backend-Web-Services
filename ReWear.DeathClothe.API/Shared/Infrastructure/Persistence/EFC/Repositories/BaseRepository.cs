using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Repositories;

public class BaseRepository<TEntity,TId> : IBaseRepository<TEntity,TId> where TEntity : class
{
    protected readonly AppDbContext Context;

    protected BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
    }

    public async Task<TEntity?> FindByIdAsync(TId id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> ListAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }
}