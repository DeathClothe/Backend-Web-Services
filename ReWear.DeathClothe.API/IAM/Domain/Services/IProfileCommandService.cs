namespace ReWear.DeathClothe.API.IAM.Domain.Services;

using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;

public interface IProfileCommandService
{
    Task<Profile> Handle(CreateProfileCommand command);
    Task<Profile?> Handle(UpdateProfileCommand command);
    Task<bool> Handle(DeleteProfileCommand command);
}