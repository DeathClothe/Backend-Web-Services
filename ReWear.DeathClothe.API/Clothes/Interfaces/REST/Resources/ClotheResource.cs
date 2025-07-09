namespace ReWear.DeathClothe.API.Clothes.Interfaces.REST.Resources;

public record ClotheResource(
    string Id,
    string Nombre,
    string Descripcion,
    int Precio,
    string Tipo,
    string Talla,
    string Color,
    int Usuario,
    string Imagen,
    List<string> Categorias,
    string ApiId
);