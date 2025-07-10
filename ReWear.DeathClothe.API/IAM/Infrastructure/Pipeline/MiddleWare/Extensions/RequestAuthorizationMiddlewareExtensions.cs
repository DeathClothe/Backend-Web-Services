using ReWear.DeathClothe.API.IAM.Infrastructure.Pipeline.MiddleWare.Components;

namespace ReWear.DeathClothe.API.IAM.Infrastructure.Pipeline.MiddleWare.Extensions;

public static class RequestAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}