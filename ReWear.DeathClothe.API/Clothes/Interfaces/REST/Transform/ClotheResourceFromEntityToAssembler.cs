using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Clothes.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.Clothes.Interfaces.REST.Transform;

public class ClotheResourceFromEntityToAssembler
{
    public static ClotheResource ToResourceFromEntity(Clothe resource)
    {
        return new ClotheResource(
            resource.Id,
            resource.Nombre,
            resource.Descripcion,
            resource.Precio,
            resource.Tipo,
            resource.Talla,
            resource.Color,
            resource.Usuario,
            resource.Imagen,
            resource.Categorias
        );
    }
}