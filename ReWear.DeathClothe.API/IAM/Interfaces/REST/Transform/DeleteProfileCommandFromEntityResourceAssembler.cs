using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;

public static class DeleteProfileCommandFromEntityResourceAssembler
{
    public static DeleteProfileCommand ToCommandFromResource(DeleteProfileResource resource)
    {
        return new DeleteProfileCommand(resource.Id);
    }
}