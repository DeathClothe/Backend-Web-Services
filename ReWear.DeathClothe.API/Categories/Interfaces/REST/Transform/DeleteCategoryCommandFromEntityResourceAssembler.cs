using ReWear.DeathClothe.API.Categories.Domain.Model.Commands;
using ReWear.DeathClothe.API.Categories.Interfaces.REST.Resources;

namespace ReWear.DeathClothe.API.Categories.Interfaces.REST.Transform;

public class DeleteCategoryCommandFromEntityResourceAssembler
{
    public static DeleteCategoryCommand ToCommandFromResource(DeleteCategoryResource resource)
    {
        return new DeleteCategoryCommand(resource.Id);
    }
}