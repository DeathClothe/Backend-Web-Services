using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;

namespace ReWear.DeathClothe.API.IAM.Domain.Services;

public interface IProfileCommandService
{
    Task Handle(SignUpCommand command);

    Task<(Profile profile, string token)> Handle(SignInCommand command);
}