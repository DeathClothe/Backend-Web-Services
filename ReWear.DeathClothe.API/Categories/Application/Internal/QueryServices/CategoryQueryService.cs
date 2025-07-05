using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Domain.Model.Queries;
using ReWear.DeathClothe.API.Categories.Domain.Repositories;
using ReWear.DeathClothe.API.Categories.Domain.Services;

namespace ReWear.DeathClothe.API.Categories.Application.Internal.QueryServices;

public class CategoryQueryService(ICategoryRepository repository)
    : ICategoryQueryService
{
    public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query)
    {
        return await repository.ListAsync();
    }
    
    public async Task<Category?> Handle(GetCategoryByIdQuery query)
    {
        return await repository.FindByIdAsync(query.Id);
    }
}
