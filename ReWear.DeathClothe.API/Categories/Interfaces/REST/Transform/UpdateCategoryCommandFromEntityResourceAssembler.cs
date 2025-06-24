using ReWear.DeathClothe.API.Categories.Domain.Model.Commands;
using ReWear.DeathClothe.API.Categories.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.Categories.Interfaces.REST.Transform;

public class UpdateCategoryCommandFromEntityResourceAssembler
{
    public static UpdateCategoryCommand ToCommandFromResource(UpdateCategoryResource resource)
    {
        return new UpdateCategoryCommand(
            resource.Id,
            resource.Nombre,
            resource.Imagen
        );
    }
}