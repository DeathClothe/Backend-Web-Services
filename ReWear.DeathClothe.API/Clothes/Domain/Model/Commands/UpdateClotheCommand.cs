namespace ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;

public record UpdateClotheCommand(int Id, string Nombre, string Descripcion, string Talla, string Color, int Precio,string Imagen,List<string> Categorias, string Usuario, string Tipo);