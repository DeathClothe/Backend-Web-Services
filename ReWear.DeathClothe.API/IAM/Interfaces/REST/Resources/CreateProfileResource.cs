namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

public record CreateProfileResource(
    string Nombre,
    string Apellidos,
    string Email,
    string Password,
    string Direccion,
    string Tipo,
    string ImageProfile
);