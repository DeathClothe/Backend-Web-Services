using Microsoft.AspNetCore.Mvc;
using ReWear.DeathClothe.API.IAM.Domain.Services;
using ReWear.DeathClothe.API.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;
using ReWear.DeathClothe.API.IAM.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace ReWear.DeathClothe.API.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(IProfileCommandService profileCommandService) : ControllerBase
{
    /**
     * <summary>
     *     Sign in endpoint. It allows authenticating a user
     * </summary>
     * <param name="signInResource">The sign-in resource containing username and password.</param>
     * <returns>The authenticated user resource, including a JWT token</returns>
     */
    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in to the platform",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "Authenticated profile", typeof(AuthenticatedProfileResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedProfile = await profileCommandService.Handle(signInCommand);
        var resource = AuthenticatedProfileResourceFromEntityAssembler
            .ToResourceFromEntity(authenticatedProfile.profile, authenticatedProfile.token);
        return Ok(resource);
    }

    /**
     * <summary>
     *     Sign up endpoint. It allows creating a new user
     * </summary>
     * <param name="signUpResource">The sign-up resource containing username and password.</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Sign up",
        Description = "Sign up to the platform",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile created successfully")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        await profileCommandService.Handle(signUpCommand);
        return Ok(new { message = "Profile created successfully" });
    }
}