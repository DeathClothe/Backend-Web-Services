using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;

namespace ReWear.DeathClothe.API.Clothes.Domain.Services;

public interface IClotheCommandService
{
    Task<Clothe> Handle(CreateClotheCommand clotheCommand);
    Task<Clothe?> Handle(UpdateClotheCommand clotheCommand);
    Task<bool> Handle(DeleteClotheCommand clotheCommand);
}