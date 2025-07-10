namespace ReWear.DeathClothe.API.IAM.Interfaces.REST.Resources;

public record ProfileResource(
    int Id,
    string Nombre,
    string Apellidos,
    string Email,
    string Direccion,
    string Tipo,
    string ImageProfile,
    List<string> Armario,
    List<string> Favoritos,
    List<string> Publicados,
    List<string> Vendidos
);