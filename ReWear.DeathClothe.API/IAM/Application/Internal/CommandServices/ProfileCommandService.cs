using ReWear.DeathClothe.API.IAM.Application.Internal.OutboundServices;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;
using ReWear.DeathClothe.API.IAM.Domain.Repositories;
using ReWear.DeathClothe.API.IAM.Domain.Services;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;

namespace ReWear.DeathClothe.API.IAM.Application.Internal.CommandServices;

public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork,
    ITokenService tokenService,
    IHashingService hashingService
    ) : IProfileCommandService
{
    public async Task Handle(SignUpCommand command)
    {
        if (profileRepository.ExistsByEmail(command.Email))
        {
            throw new Exception($"Username {command.Email} already exists");
        }
        
        var hashedPassword = hashingService.HashPassword(command.Password);
        var profile = new Profile(command, hashedPassword);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error ocurred while creating the profile: {e.Message}");
        }
    }

    public async Task<(Profile profile, string token)> Handle(SignInCommand command)
    {
        var profile = await profileRepository.FindByEmailAsync(command.Email);

        if (profile is null)
        {
            throw new Exception($"Profile {command.Email} not found");
        }

        if (!hashingService.VerifyPassword(command.Password, profile.PasswordHash))
        {
            throw new Exception("Invalid password");
        }

        var token = tokenService.GenerateToken(profile);
        return (profile, token);
    }
    
    public async Task<Profile> Handle(UpdateProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.Id);
        if (profile is null)
            throw new Exception($"Profile with id {command.Id} not found");

        // Actualiza los campos de texto
        profile.GetType().GetProperty("Nombre")?.SetValue(profile, command.Nombre);
        profile.GetType().GetProperty("Apellidos")?.SetValue(profile, command.Apellidos);
        profile.GetType().GetProperty("Direccion")?.SetValue(profile, command.Direccion);
        profile.GetType().GetProperty("Tipo")?.SetValue(profile, command.Tipo);
        profile.GetType().GetProperty("ImageProfile")?.SetValue(profile, command.ImageProfile);

        // Ahora también actualiza los arrays
        profile.GetType().GetProperty("Armario")?.SetValue(profile, command.Armario);
        profile.GetType().GetProperty("Favoritos")?.SetValue(profile, command.Favoritos);
        profile.GetType().GetProperty("Publicados")?.SetValue(profile, command.Publicados);
        profile.GetType().GetProperty("Vendidos")?.SetValue(profile, command.Vendidos);

        await profileRepository.UpdateAsync(profile);
        await unitOfWork.CompleteAsync();
        return profile;
    }

}