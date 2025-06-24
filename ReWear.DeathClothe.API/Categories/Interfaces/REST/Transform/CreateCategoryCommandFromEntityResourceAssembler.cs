using ReWear.DeathClothe.API.Categories.Domain.Model.Commands;
using ReWear.DeathClothe.API.Categories.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.Categories.Interfaces.REST.Transform;

public class CreateCategoryCommandFromEntityResourceAssembler
{
    public static CreateCategoryCommand ToCommandFromResource(CreateCategoryResource resource)
    {
        return new CreateCategoryCommand(
            resource.Nombre,
            resource.Imagen
        );
    }
}