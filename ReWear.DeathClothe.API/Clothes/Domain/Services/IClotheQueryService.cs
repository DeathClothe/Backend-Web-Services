using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Queries;

namespace ReWear.DeathClothe.API.Clothes.Domain.Services;

public interface IClotheQueryService
{
    Task<IEnumerable<Clothe>> Handle(GetAllClothesQuery query);
    Task<Clothe?> Handle(GetClotheByIdQuery query);

}