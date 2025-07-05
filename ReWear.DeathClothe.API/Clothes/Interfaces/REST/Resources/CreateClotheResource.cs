namespace ReWear.DeathClothe.API.Clothes.Interfaces.REST.Resources;

public record CreateClotheResource(
    string Nombre,
    string Descripcion,
    int Precio,
    string Tipo,
    string Talla,
    string Color,
    int Usuario,
    string Imagen,
    List<string> Categorias
);