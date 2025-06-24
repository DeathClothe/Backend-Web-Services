using ReWear.DeathClothe.API.Categories.Domain.Model.Commands;

namespace ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;

public partial class Category
{
    public int Id { get; private set; }
    
    public string Nombre { get; private set; }
    
    public string Imagen { get; private set; }

    public Category(
        string nombre,
        string imagen)
    {
        Nombre = nombre;
        Imagen = imagen;
    }

    public Category(CreateCategoryCommand command)
        : this(command.Nombre, command.Imagen)
    {
    }

    public void UpdateFromCommand(UpdateCategoryCommand command)
    {
        Nombre = command.Nombre;
        Imagen = command.Imagen;
    }
}