using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace ReWear.DeathClothe.API.Categories.Infrastructure.Persistence.EFC.Repositories;

public class CategoryRepository(AppDbContext context)
    : BaseRepository<Category>(context), ICategoryRepository
{
}