using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;
using ReWear.DeathClothe.API.Clothes.Domain.Repositories;
using ReWear.DeathClothe.API.Clothes.Domain.Services;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;

namespace ReWear.DeathClothe.API.Clothes.Application.Internal.CommandServices;

public class ClotheCommandService(IClotheRepository clotheRepository, IUnitOfWork unitOfWork): IClotheCommandService
{
    public async Task<Clothe> Handle(CreateClotheCommand command)
    {
        var clothe = new Clothe(command);
        try
        {
            await clotheRepository.AddAsync(clothe);
            await unitOfWork.CompleteAsync();
            return clothe;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<Clothe?> Handle(UpdateClotheCommand command)
    {
        var clothe = await clotheRepository.FindByIdAsync(command.Id);
        if (clothe == null) return null;
        try
        {
            clothe.UpdateFromCommand(command);
            await unitOfWork.CompleteAsync();
            return clothe;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> Handle(DeleteClotheCommand command)
    {
        var clothe = await clotheRepository.FindByIdAsync(command.Id);
        if (clothe == null) return false;
        try
        {
            clotheRepository.Delete(clothe);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}