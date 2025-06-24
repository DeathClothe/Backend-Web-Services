using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;
using ReWear.DeathClothe.API.IAM.Domain.Repositories;
using ReWear.DeathClothe.API.IAM.Domain.Services;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;

namespace ReWear.DeathClothe.API.IAM.Application.Internal.CommandServices;

public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork) : IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    public async Task<Profile?> Handle(UpdateProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.Id);
        if (profile == null) return null;
        try
        {
            profile.UpdateFromCommand(command);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> Handle(DeleteProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.Id);
        if (profile == null) return false;
        try
        {
            profileRepository.Delete(profile);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}