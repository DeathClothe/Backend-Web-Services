namespace ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;

public record UpdateClotheCommand(
    string Id,
    string? Nombre,
    string? Descripcion,
    string? Talla,
    string? Color,
    int? Precio,
    string? Imagen,
    List<string>? Categorias,
    int Usuario,  
    string? Tipo);
