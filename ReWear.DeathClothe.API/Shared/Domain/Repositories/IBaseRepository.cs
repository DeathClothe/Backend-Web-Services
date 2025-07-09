namespace ReWear.DeathClothe.API.Shared.Domain.Repositories;

public interface IBaseRepository<TEntity, TId>
{
    Task AddAsync(TEntity entity);
    Task<TEntity?> FindByIdAsync(TId id);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<IEnumerable<TEntity>> ListAsync();
}