using ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;
using ReWear.DeathClothe.API.Clothes.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.Clothes.Interfaces.REST.Transform;

public class UpdateClotheCommandFromEntityToResourceAssembler
{
    public static UpdateClotheCommand ToCommandFromResource(int id, UpdateClotheResource resource)
    {
        return new UpdateClotheCommand(
            id,
            resource.Nombre,
            resource.Descripcion,
            resource.Talla,
            resource.Color,
            resource.Precio,
            resource.Imagen,
            resource.Categorias,
            resource.Usuario,
            resource.Tipo
        );
    }
}