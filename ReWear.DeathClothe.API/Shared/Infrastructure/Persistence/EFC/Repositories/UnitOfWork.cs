using ReWear.DeathClothe.API.Shared.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork 
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}