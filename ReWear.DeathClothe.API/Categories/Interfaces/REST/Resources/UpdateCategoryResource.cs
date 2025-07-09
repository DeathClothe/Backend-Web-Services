namespace ReWear.DeathClothe.API.Categories.Interfaces.REST.Resources;

public record UpdateCategoryResource(
    string Id,
    string Nombre,
    string Imagen
);