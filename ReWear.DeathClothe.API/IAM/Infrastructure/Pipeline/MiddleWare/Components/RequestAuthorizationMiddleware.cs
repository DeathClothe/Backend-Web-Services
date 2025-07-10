using ReWear.DeathClothe.API.IAM.Application.Internal.OutboundServices;
using ReWear.DeathClothe.API.IAM.Domain.Model.Queries;
using ReWear.DeathClothe.API.IAM.Domain.Services;
using ReWear.DeathClothe.API.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;

namespace ReWear.DeathClothe.API.IAM.Infrastructure.Pipeline.MiddleWare.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IProfileQueryService profileQueryService,
        ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        var allowAnonymous = context.Request.HttpContext.GetEndpoint()!
            .Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
        Console.WriteLine($"AllowAnonymous: {allowAnonymous}");
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            await next(context);
            return;
        }
        Console.WriteLine("Entering authorization");
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token is null) throw new Exception("Null of invalid token");

        var id = await tokenService.ValidateToken(token);
        
        if (id is null) throw new Exception("Invalid token");

        var getProfileByIdQuery = new GetProfileByIdQuery(id.Value);
        var profile = await profileQueryService.Handle(getProfileByIdQuery);
        Console.WriteLine("Successfully authorized. Updating context...");
        context.Items["Profile"] = profile;
        Console.WriteLine("Continuing to next middleware in pipeline");
        await next(context);
    }
}