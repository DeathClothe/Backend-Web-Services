using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;

public static class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile profile)
    {
        return new ProfileResource(
            profile.Id,
            profile.Nombre,
            profile.Password,
            profile.Apellidos,
            profile.Email,
            profile.Direccion,
            profile.Tipo,
            profile.ImageProfile,
            profile.Armario,
            profile.Favoritos,
            profile.Publicados,
            profile.Vendidos
        );
    }
}