namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

public record UpdateProfileResource(
    string Nombre,
    string Apellidos,
    string Direccion,
    string Tipo,
    string ImageProfile
);