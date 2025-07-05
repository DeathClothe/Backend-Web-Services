using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Domain.Model.Commands;

namespace ReWear.DeathClothe.API.Categories.Domain.Services;

public interface ICategoryCommandService
{
    Task<Category> Handle(CreateCategoryCommand command);
    Task<Category?> Handle(UpdateCategoryCommand command);
    Task<bool> Handle(DeleteCategoryCommand command);
}