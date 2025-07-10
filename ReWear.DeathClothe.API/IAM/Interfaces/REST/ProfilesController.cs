using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ReWear.DeathClothe.API.IAM.Domain.Model.Queries;
using ReWear.DeathClothe.API.IAM.Domain.Services;
using ReWear.DeathClothe.API.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile endpoints")]
public class ProfilesController(IProfileQueryService profileQueryService) : ControllerBase
{
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get profile by id",
        Description = "Get profile by id",
        OperationId = "GetProfileById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile found", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> GetProfileById(int id)
    {
        var getProfileByIdQuery = new GetProfileByIdQuery(id);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        if (profile is null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all profiles",
        Description = "Get all profiles",
        OperationId = "GetAllProfiles")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profiles found", typeof(IEnumerable<ProfileResource>))]
    public async Task<IActionResult> GetAllProfiles()
    {
        var getAllProfilesQuery = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(getAllProfilesQuery);
        var profileResources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(profileResources);
    }
}