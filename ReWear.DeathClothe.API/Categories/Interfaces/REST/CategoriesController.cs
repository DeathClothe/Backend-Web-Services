using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReWear.DeathClothe.API.Categories.Domain.Model.Commands;
using ReWear.DeathClothe.API.Categories.Domain.Model.Queries;
using ReWear.DeathClothe.API.Categories.Domain.Services;
using ReWear.DeathClothe.API.Categories.Interfaces.REST.Resources;
using ReWear.DeathClothe.API.Categories.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace ReWear.DeathClothe.API.Categories.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Categories endpoints")]
public class CategoriesController(
    ICategoryCommandService categoryCommandService,
    ICategoryQueryService categoryQueryService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all categories",
        Description = "Get all categories",
        OperationId = "GetAllCategories")]
    [SwaggerResponse(200, "Returns all categories", typeof(IEnumerable<CategoryResource>))]
    [SwaggerResponse(404, "The categories were not found")]
    public async Task<IActionResult> GetAllCategories()
    {
        var getAllCategoriesQuery = new GetAllCategoriesQuery();
        var categories = await categoryQueryService.Handle(new GetAllCategoriesQuery());
        var categoryResources = categories.Select(CategoryResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(categoryResources);
    }

    [HttpGet("{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Get category by ID",
        Description = "Get category by ID",
        OperationId = "GetCategoryById")]
    [SwaggerResponse(200, "Return category", typeof(IEnumerable<CategoryResource>))]
    [SwaggerResponse(404, "Category not found")]
    public async Task<IActionResult> GetCategoryById(int categoryId)
    {
        var category = await categoryQueryService.Handle(new GetCategoryByIdQuery(categoryId));
        if (category is null)
        {
            return NotFound();
        }
        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(category);
        return Ok(categoryResource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new Category",
        Description = "Create a new Category",
        OperationId = "CreateCategory")]
    [SwaggerResponse(200, "Created Category", typeof(IEnumerable<CategoryResource>))]
    [SwaggerResponse(400, "Category could not be created")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryResource resource)
    {
        var createCategoryCommand = CreateCategoryCommandFromEntityResourceAssembler.ToCommandFromResource(resource);
        var createdCategory = await categoryCommandService.Handle(createCategoryCommand);
        if (createdCategory is null)
        {
            return BadRequest();
        }
        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(createdCategory);
        return CreatedAtAction(nameof(GetCategoryById), new { categoryId = createdCategory.Id }, categoryResource);
    }

    [HttpPut("{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Update category",
        Description = "Update a category",
        OperationId = "UpdateCategory")]
    [SwaggerResponse(200, "Category updated successfully", typeof(CategoryResource))]
    [SwaggerResponse(404, "Category not found")]
    public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryResource resource)
    {
        if (categoryId != resource.Id) return BadRequest();
        var command = UpdateCategoryCommandFromEntityResourceAssembler.ToCommandFromResource(resource);
        var updatedCategory = await categoryCommandService.Handle(command);
        if (updatedCategory is null) return NotFound();
        var categoryResource = CategoryResourceFromEntityAssembler.ToResourceFromEntity(updatedCategory);
        return Ok(categoryResource);
    }

    [HttpDelete("{categoryId:int}")]
    [SwaggerOperation(
        Summary = "Delete category",
        Description = "Delete a category",
        OperationId = "DeleteCategory")]
    [SwaggerResponse(204, "Category deleted successfully")]
    [SwaggerResponse(404, "Category not found")]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        var command = new DeleteCategoryCommand(categoryId);
        var result = await categoryCommandService.Handle(command);
        if (!result) return NotFound();
        return NoContent();
    }
}