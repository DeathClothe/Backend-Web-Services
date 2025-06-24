namespace ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;

public record CreateClotheCommand(
    string Nombre,
    string Descripcion,
    int Precio,
    string Tipo,
    string Talla,
    string Color,
    int Usuario,
    string Imagen,
    List<string> Categorias);
