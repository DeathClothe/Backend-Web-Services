using ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;

namespace ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;

public class Clothe
{
    public int Id { get; }
    public string Nombre { get; private set; }
    
    public string Descripcion { get; private set; }
    
    public int Precio { get; private set; }
    
    public string Tipo {get; private set;}
    
    public string Talla {get; private set;}
    
    public string Color  {get; private set;}
    
    public int Usuario {get; private set;}
    
    public string Imagen {get; private set;}
    
    public List<string> Categorias {get; private set;}

    public Clothe(string nombre, string descripcion, int precio, string tipo, string talla, string color, int usuario,
        string imagen)
    {
       Nombre = nombre;
       Descripcion = descripcion;
       Precio = precio;
       Tipo = tipo;
       Talla = talla;
       Color = color;
       Usuario = usuario;
       Imagen = imagen;
        
        Categorias = new List<string>();
    }

    public Clothe(CreateClotheCommand command)
        : this(command.Nombre, command.Descripcion, command.Precio,
            command.Tipo, command.Talla, command.Color, command.Usuario,
            command.Imagen)
    {
        Categorias = command.Categorias ?? new List<string>();
    }
    public void UpdateFromCommand(UpdateClotheCommand command)
    {
        Nombre = command.Nombre;
        Descripcion = command.Descripcion;
        Precio = command.Precio;
        Tipo = command.Tipo;
        Talla = command.Talla;
        Color = command.Color;
        Imagen = command.Imagen;
        Categorias = command.Categorias ?? new List<string>();
    }

}