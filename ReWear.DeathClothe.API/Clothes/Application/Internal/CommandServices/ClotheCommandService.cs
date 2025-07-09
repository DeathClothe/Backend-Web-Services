using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;
using ReWear.DeathClothe.API.Clothes.Domain.Model.ValueObjects;
using ReWear.DeathClothe.API.Clothes.Domain.Repositories;
using ReWear.DeathClothe.API.Clothes.Domain.Services;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Services;

namespace ReWear.DeathClothe.API.Clothes.Application.Internal.CommandServices;

public class ClotheCommandService(IClotheRepository clotheRepository, IUnitOfWork unitOfWork): IClotheCommandService
{
    public async Task<Clothe> Handle(CreateClotheCommand command)
    {
        try
        {
            var lastId = await clotheRepository.GetLastClotheIdAsync();
            var newId = CustomIdGenerator.GenerateNextId(lastId, "P");
           
            var clothe = new Clothe(
                newId,
                command.Nombre,
                command.Descripcion,
                command.Precio,
                command.Tipo,
                command.Talla,
                command.Color,
                command.Usuario,
                command.Imagen
            );


           
            clothe.Categorias = command.Categorias ?? new List<string>();

            await clotheRepository.AddAsync(clothe);
            await unitOfWork.CompleteAsync();
            return clothe;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creando prenda: {e.Message}");
            throw new InvalidOperationException($"Error al crear la prenda: {e.Message}", e);
        }
    }


    public async Task<Clothe?> Handle(UpdateClotheCommand command)
    {
        var clothe = await clotheRepository.FindByIdAsync(command.Id);
        if (clothe == null) return null;

        try
        {
            if (!string.IsNullOrWhiteSpace(command.Nombre))
                clothe.Nombre = command.Nombre;

            if (!string.IsNullOrWhiteSpace(command.Descripcion))
                clothe.Descripcion = command.Descripcion;

            if (!string.IsNullOrWhiteSpace(command.Tipo))
                clothe.Tipo = command.Tipo;

            if (!string.IsNullOrWhiteSpace(command.Talla))
                clothe.Talla = new Talla(command.Talla); 

            if (!string.IsNullOrWhiteSpace(command.Color))
                clothe.Color = new Color(command.Color); 

            if (command.Precio is >= 0)
                clothe.Precio = command.Precio.Value;

            if (!string.IsNullOrWhiteSpace(command.Imagen))
                clothe.Imagen = command.Imagen;

            if (command.Categorias is not null)
                clothe.Categorias = command.Categorias;

            await unitOfWork.CompleteAsync();
            return clothe;
        }
        catch (Exception)
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