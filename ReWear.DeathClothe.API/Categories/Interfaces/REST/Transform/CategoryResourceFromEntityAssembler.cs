using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.Categories.Interfaces.REST.Transform;

public class CategoryResourceFromEntityAssembler
{
    public static CategoryResource ToResourceFromEntity(Category category)
    {
        return new CategoryResource(
            category.Id,
            category.Nombre,
            category.Imagen
        );
    }
}