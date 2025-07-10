using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;

public class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(int id, UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(
            id,
            resource.Nombre,
            resource.Apellidos,
            resource.Direccion,
            resource.Tipo,
            resource.ImageProfile
        );
    }
}