namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

public record UpdateProfileResource(
    string Nombre,
    string Apellidos,
    string Direccion,
    string Tipo,
    string ImageProfile,
    List<string> Armario,
    List<string> Favoritos,
    List<string> Publicados,
    List<string> Vendidos
);