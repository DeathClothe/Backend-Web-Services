using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Domain.Model.Queries;

namespace ReWear.DeathClothe.API.Categories.Domain.Services;

public interface ICategoryQueryService
{
    Task<Category?> Handle(GetCategoryByIdQuery query);
    
    Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery query);
}