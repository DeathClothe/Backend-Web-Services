using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using ReWear.DeathClothe.API.IAM.Domain.Model.Queries;
using ReWear.DeathClothe.API.IAM.Domain.Services;
using ReWear.DeathClothe.API.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST;

/**
 * <summary>
 *     The profile's controller
 * </summary>
 * <remarks>
 *     This class is used to handle profile requests
 * </remarks>
 */
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile endpoints")]
public class ProfilesController(IProfileQueryService profileQueryService) : ControllerBase
{
    /**
     * <summary>
     *     Get profile by id
     * </summary>
     * <param name="id">The profile id</param>
     * <returns>The profile resource</returns>
     */
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

    /**
     * <summary>
     *     Get all profiles
     * </summary>
     * <returns>The profile resources</returns>
     */
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
    
    /**
     * <summary>
     *     Update profile by id
     * </summary>
     * <param name="id">The profile id</param>
     * <param name="resource">The profile resource</param>
     * <returns>The updated profile resource</returns>
     */
    [HttpPut("{id:int}")]
    [SwaggerOperation(
        Summary = "Update profile by id",
        Description = "Update profile by id",
        OperationId = "UpdateProfileById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile updated", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileResource resource, [FromServices] IProfileCommandService profileCommandService)
    {
        try
        {
            var command = UpdateProfileCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var updatedProfile = await profileCommandService.Handle(command);
            var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(updatedProfile);
            return Ok(profileResource);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}