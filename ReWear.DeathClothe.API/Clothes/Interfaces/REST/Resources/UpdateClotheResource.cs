namespace ReWear.DeathClothe.API.Clothes.Interfaces.REST.Resources;

public record UpdateClotheResource(
    string Nombre,
    string Descripcion,
    string Talla,
    string Color,
    int Precio,
    string Imagen,
    List<string> Categorias,
    string Usuario,
    string Tipo
);
