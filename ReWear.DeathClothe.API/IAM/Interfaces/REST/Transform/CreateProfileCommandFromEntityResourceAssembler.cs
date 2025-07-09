using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;

public static class CreateProfileCommandFromEntityResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(
            resource.Nombre,
            resource.Apellidos,
            resource.Email,
            resource.Password,
            resource.Direccion,
            resource.Tipo
        );
    }
}