namespace ReWear.DeathClothe.API.IAM.Domain.Model.Commands;

public record SignUpCommand(
    string Nombre,
    string Apellidos,
    string Email,
    string Password,
    string Direccion,
    string Tipo,
    string ImageProfile
    );