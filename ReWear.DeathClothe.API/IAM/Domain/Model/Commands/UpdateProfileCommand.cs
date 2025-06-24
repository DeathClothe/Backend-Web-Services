namespace ReWear.DeathClothe.API.IAM.Domain.Model.Commands;

public record UpdateProfileCommand(
    int Id,
    string Nombre,
    string Apellidos,
    string Email,
    string Password,
    string Direccion,
    string Tipo,
    string ImageProfile
);