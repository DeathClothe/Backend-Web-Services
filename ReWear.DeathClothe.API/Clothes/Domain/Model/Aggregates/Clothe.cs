using ReWear.DeathClothe.API.Clothes.Domain.Model.Commands;
using ReWear.DeathClothe.API.Clothes.Domain.Model.ValueObjects;

namespace ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;

public partial class Clothe
{
    public string  Id { get; set; }
    public string Nombre { get; set; }
    
    public string Descripcion { get; set; }
    
    public int Precio { get; set; }
    
    public string Tipo {get; set;}
    
    public Talla Talla { get; set; }
    public Color Color { get; set; }
    
    public int Usuario {get; set;}
    
    public string Imagen {get; set;}
    
    public List<string> Categorias {get; set;}
    
    public string ApiId { get; set; } = "v1";

    public Clothe()
    {
    }

    public Clothe(string id,string nombre, string descripcion, int precio, 
        string tipo, string talla,
        string color, int usuario,
        string imagen)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(tipo))
            throw new ArgumentException("El tipo es obligatorio.");
        if (precio < 0)
        {
            throw new ArgumentException("Precio no puede ser negativo");
        }
       
        Id = id ?? throw new ArgumentException("Id no puede ser null");
       Nombre = nombre;
       Descripcion = descripcion ?? "";
       Precio = precio;
       Tipo = tipo;
       Talla = new Talla(talla);
       Color = new  Color(color);
       Usuario = usuario;
       Imagen =  imagen ?? "";
       Categorias = new List<string>();
       ApiId = "v1";

    }

   

}