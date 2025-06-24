using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;
using Swashbuckle.AspNetCore.Annotations;
using ReWear.DeathClothe.API.IAM.Domain.Services;
using ReWear.DeathClothe.API.IAM.Domain.Model.Queries;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile endpoints")]
public class ProfilesController(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all profiles",
        Description = "Get all profiles",
        OperationId = "GetAllProfiles")]
    [SwaggerResponse(200, "Returns all profiles", typeof(IEnumerable<ProfileResource>))]
    [SwaggerResponse(404, "The profiles were not found")]
    public async Task<IActionResult> GetAllProfiles()
    {
        var getAllProfilesQuery = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(new GetAllProfilesQuery());
        var profileResources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(profileResources);
    }

    [HttpGet("{profileId:int}")]
    [SwaggerOperation(
        Summary = "Get profile by ID",
        Description = "Get profile by ID",
        OperationId = "GetProfileById")]
    [SwaggerResponse(200, "Return profile", typeof(IEnumerable<ProfileResource>))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> GetProfileById(int profileId)
    {
        var profile = await profileQueryService.Handle(new GetProfileByIdQuery(profileId));
        if (profile is null)
        {
            return NotFound();
        }
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }
    
    [HttpGet("email/{email}")]
    [SwaggerOperation(
        Summary = "Get profile by email",
        Description = "Get profile by email",
        OperationId = "GetProfileByEmail")]
    [SwaggerResponse(200, "Return profile", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> GetProfileByEmail(string email)
    {
        var profile = await profileQueryService.Handle(new GetProfileByEmailQuery(email));
        if (profile is null)
        {
            return NotFound();
        }
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return Ok(profileResource);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new Profile",
        Description = "Create a new Profile",
        OperationId = "CreateProfile")]
    [SwaggerResponse(200, "Created Profile", typeof(IEnumerable<ProfileResource>))]
    [SwaggerResponse(400, "Profile could not be created")]
    public async Task<IActionResult> CreateProfile([FromBody] CreateProfileResource resource)
    {
        var createProfileCommand = CreateProfileCommandFromEntityResourceAssembler.ToCommandFromResource(resource);
        var createdProfile = await profileCommandService.Handle(createProfileCommand);
        if (createdProfile is null)
        {
            return BadRequest();
        }
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(createdProfile);
        return CreatedAtAction(nameof(GetProfileById), new { profileId = createdProfile.Id }, profileResource);
    }

    [HttpPut("{profileId:int}")]
    [SwaggerOperation(
        Summary = "Update profile",
        Description = "Update a profile",
        OperationId = "UpdateProfile")]
    [SwaggerResponse(200, "Profile updated successfully", typeof(ProfileResource))]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> UpdateProfile(int profileId, [FromBody] UpdateProfileResource resource)
    {
        if (profileId != resource.Id) return BadRequest();
        var command = UpdateProfileCommandFromEntityResourceAssembler.ToCommandFromResource(resource);
        var updatedProfile = await profileCommandService.Handle(command);
        if (updatedProfile is null) return NotFound();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(updatedProfile);
        return Ok(profileResource);
    }

    [HttpDelete("{profileId:int}")]
    [SwaggerOperation(
        Summary = "Delete profile",
        Description = "Delete a profile",
        OperationId = "DeleteProfile")]
    [SwaggerResponse(204, "Profile deleted successfully")]
    [SwaggerResponse(404, "Profile not found")]
    public async Task<IActionResult> DeleteProfile(int profileId)
    {
        var command = new DeleteProfileCommand(profileId);
        var result = await profileCommandService.Handle(command);
        if (!result) return NotFound();
        return NoContent();
    }
}