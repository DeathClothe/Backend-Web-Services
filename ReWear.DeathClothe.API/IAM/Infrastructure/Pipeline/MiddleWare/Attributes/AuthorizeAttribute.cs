using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;

namespace ReWear.DeathClothe.API.IAM.Infrastructure.Pipeline.MiddleWare.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            return;
        }
        var profile = (Profile?)context.HttpContext.Items["Profile"];
        if (profile is null) context.Result = new UnauthorizedResult();
    }
}