using ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;
using ReWear.DeathClothe.API.Clothes.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.Clothes.Interfaces.REST.Transform;

public class DeleteClotheCommandFromEntityToResourceAssembler
{
    public static DeleteClotheCommand ToCommandFromResource(DeleteClotheResource resource)
    {
        return new DeleteClotheCommand(resource.Id);
    }
}