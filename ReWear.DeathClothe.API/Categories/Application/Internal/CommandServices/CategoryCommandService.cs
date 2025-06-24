using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Domain.Model.Commands;
using ReWear.DeathClothe.API.Categories.Domain.Repositories;
using ReWear.DeathClothe.API.Categories.Domain.Services;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;

namespace ReWear.DeathClothe.API.Categories.Application.Internal.CommandServices;

public class CategoryCommandService(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork) : ICategoryCommandService
{
    public async Task<Category?> Handle(CreateCategoryCommand command)
    {
        var category = new Category(command);
        try
        {
            await categoryRepository.AddAsync(category);
            await unitOfWork.CompleteAsync();
            return category;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    public async Task<Category?> Handle(UpdateCategoryCommand command)
    {
        var category = await categoryRepository.FindByIdAsync(command.Id);
        if (category == null) return null;
        try
        {
            category.UpdateFromCommand(command);
            await unitOfWork.CompleteAsync();
            return category;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> Handle(DeleteCategoryCommand command)
    {
        var category = await categoryRepository.FindByIdAsync(command.Id);
        if (category == null) return false;
        try
        {
            categoryRepository.Delete(category);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
