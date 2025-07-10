using Microsoft.AspNetCore.Mvc;
using ReWear.DeathClothe.API.IAM.Domain.Services;
using ReWear.DeathClothe.API.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class AuthenticationController(IProfileCommandService profileCommandService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in to the platform",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "Authenticated profile", typeof(AuthenticatedProfileResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var authenticatedProfile = await profileCommandService.Handle(signInCommand);
        var authenticatedProfileResource = AuthenticatedProfileResourceFromEntityAssembler
            .ToResourceFromEntity(authenticatedProfile.profile, authenticatedProfile.token);
        return Ok(authenticatedProfileResource);
    }

    [AllowAnonymous]
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Sign up",
        Description = "Sign up to the platform",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile created successfully")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        await profileCommandService.Handle(signUpCommand);
        return Ok(new { message = "Profile created successfully" });
    }
    
    
}