using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;

namespace ReWear.DeathClothe.API.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(Profile profile);

    Task<int?> ValidateToken(string token);
}