namespace ReWear.DeathClothe.API.IAM.Domain.Model.Commands;

public record UpdateProfileCommand(
    int Id,
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