using ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;
using ReWear.DeathClothe.API.Clothes.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.Clothes.Interfaces.REST.Transform;

public class CreateClotheCommandFromEntityToResourceAssembler
{
    public static CreateClotheCommand ToCommandFromResource(CreateClotheResource resource)
    {
        return new CreateClotheCommand(
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