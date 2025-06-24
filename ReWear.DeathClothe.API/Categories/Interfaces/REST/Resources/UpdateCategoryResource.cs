namespace ReWear.DeathClothe.API.Categories.Interfaces.REST.Resources;

public record UpdateCategoryResource(
    int Id,
    string Nombre,
    string Imagen
);