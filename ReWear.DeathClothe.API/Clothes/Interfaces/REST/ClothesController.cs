using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Queries;
using ReWear.DeathClothe.API.Clothes.Domain.Services;
using ReWear.DeathClothe.API.Clothes.Interfaces.REST.Resources;
using ReWear.DeathClothe.API.Clothes.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace ReWear.DeathClothe.API.Clothes.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Clothes endpoints")]
public class ClothesController(IClotheCommandService clotheCommandService, IClotheQueryService clotheQueryService)
    : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Clothes",
        Description = "Get All Clothes",
        OperationId = "GetAllClothes")]
    [SwaggerResponse(200, "Return All Clothes", typeof(IEnumerable<ClotheResource>))]
    [SwaggerResponse(500, "Internal Server Error", null)]
    [SwaggerResponse(404, "The Clothes were not found")]
    public async Task<IActionResult> GetAllClothes()
    {
        var clothes = await clotheQueryService.Handle(new GetAllClothesQuery());
        var clotheResources = clothes.Select(ClotheResourceFromEntityToAssembler.ToResourceFromEntity);
        return Ok(clotheResources);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new Clothe",
        Description = "Create a new Clothe",
        OperationId = "CreateClothe")]
    [SwaggerResponse(200, "Created Clothe", typeof(ClotheResource))]
    [SwaggerResponse(400, "Clothe could not be created")]
    public async Task<IActionResult> CreateClothe([FromBody] CreateClotheResource resource)
    {
        var createCommand = CreateClotheCommandFromEntityToResourceAssembler.ToCommandFromResource(resource);
        var createdClothe = await clotheCommandService.Handle(createCommand);
        if (createdClothe is null) return BadRequest();
        var clotheResource = ClotheResourceFromEntityToAssembler.ToResourceFromEntity(createdClothe);
        return CreatedAtAction(nameof(GetClotheById), new { clotheId = createdClothe.Id }, clotheResource);
    }

    [HttpGet("{clotheId:int}")]
    [SwaggerOperation(
        Summary = "Get Clothe by ID",
        Description = "Get Clothe by ID",
        OperationId = "GetClotheById")]
    [SwaggerResponse(200, "Return Clothe", typeof(ClotheResource))]
    [SwaggerResponse(404, "Clothe not found")]
    public async Task<IActionResult> GetClotheById(int clotheId)
    {
        var clothe = await clotheQueryService.Handle(new GetClotheByIdQuery(clotheId));
        if (clothe is null) return NotFound();
        var clotheResource = ClotheResourceFromEntityToAssembler.ToResourceFromEntity(clothe);
        return Ok(clotheResource);
    }

    [HttpPut("{clotheId:int}")]
    [SwaggerOperation(
        Summary = "Update Clothe",
        Description = "Update a Clothe",
        OperationId = "UpdateClothe")]
    [SwaggerResponse(200, "Clothe updated successfully", typeof(ClotheResource))]
    [SwaggerResponse(404, "Clothe not found")]
    public async Task<IActionResult> UpdateClothe(int clotheId, [FromBody] UpdateClotheResource resource)
    {
        var command = UpdateClotheCommandFromEntityToResourceAssembler.ToCommandFromResource(clotheId, resource);
        var updatedClothe = await clotheCommandService.Handle(command);
        if (updatedClothe is null) return NotFound();
        var clotheResource = ClotheResourceFromEntityToAssembler.ToResourceFromEntity(updatedClothe);
        return Ok(clotheResource);
    }

    [HttpDelete("{clotheId:int}")]
    [SwaggerOperation(
        Summary = "Delete Clothe",
        Description = "Delete a Clothe",
        OperationId = "DeleteClothe")]
    [SwaggerResponse(204, "Clothe deleted successfully")]
    [SwaggerResponse(404, "Clothe not found")]
    public async Task<IActionResult> DeleteClothe(int clotheId)
    {
        var command = new DeleteClotheCommand(clotheId);
        var result = await clotheCommandService.Handle(command);
        if (!result) return NotFound();
        return NoContent();
    }

}
