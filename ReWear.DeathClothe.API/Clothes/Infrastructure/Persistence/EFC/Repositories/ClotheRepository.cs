using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Clothes.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace ReWear.DeathClothe.API.Clothes.Infrastructure.Persistence.EFC.Repositories;

public class ClotheRepository(AppDbContext context): BaseRepository<Clothe>(context), IClotheRepository
 
{
    
}