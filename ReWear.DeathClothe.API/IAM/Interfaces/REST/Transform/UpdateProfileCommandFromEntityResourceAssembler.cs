using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;


public static class UpdateProfileCommandFromEntityResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(
            resource.Id,
            resource.Nombre,
            resource.Apellidos,
            resource.Email,
            resource.Password,
            resource.Direccion,
            resource.Tipo,
            resource.ImageProfile
        );
    }
}