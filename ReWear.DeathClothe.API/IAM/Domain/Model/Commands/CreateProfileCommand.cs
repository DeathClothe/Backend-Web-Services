namespace ReWear.DeathClothe.API.IAM.Domain.Model.Commands;

public record CreateProfileCommand(
    string Nombre,
    string Apellidos,
    string Email,
    string Password,
    string Direccion,
    string Tipo,
    string ImageProfile
);