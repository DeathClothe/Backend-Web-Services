using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;

public static class AuthenticatedProfileResourceFromEntityAssembler
{
    public static AuthenticatedProfileResource ToResourceFromEntity(Profile profile, string token)
    {
        return new AuthenticatedProfileResource(profile.Id, profile.Email, token);
    }
}