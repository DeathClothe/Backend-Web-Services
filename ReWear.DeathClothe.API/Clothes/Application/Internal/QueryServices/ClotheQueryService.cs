using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Queries;
using ReWear.DeathClothe.API.Clothes.Domain.Repositories;
using ReWear.DeathClothe.API.Clothes.Domain.Services;

namespace ReWear.DeathClothe.API.Clothes.Application.Internal.QueryServices;

public class ClotheQueryService(IClotheRepository repository): IClotheQueryService
{
    public async Task<IEnumerable<Clothe>>Handle(GetAllClothesQuery query)
    {
        return await repository.ListAsync();
    }
    
    public async Task<Clothe?> Handle(GetClotheByIdQuery query)
    {
        return await repository.FindByIdAsync(query.Id);
    }
}