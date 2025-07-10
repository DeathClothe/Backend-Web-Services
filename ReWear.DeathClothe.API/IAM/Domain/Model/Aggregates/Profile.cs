using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;

namespace ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;

public partial class Profile
{
    public int Id { get; private set; }
    
    public string Nombre { get; private set; }
    
    public string Apellidos { get; private set; }
    
    public string Email { get; private set; }
    
    public string PasswordHash { get; private set; }
    
    public string Direccion { get; private set; }
    
    public string Tipo { get; private set; }
    
    public string ImageProfile { get; private set; }
    
    public List<string> Armario { get; private set; }
    
    public List<string> Favoritos { get; private set; }
    
    public List<string> Publicados { get; private set; }
    
    public List<string> Vendidos { get; private set; }
    
    public Profile( string nombre,
        string apellidos,
        string email,
        string passwordHash,
        string direccion,
        string tipo)
    {
        Nombre = nombre;
        Apellidos = apellidos;
        Email = email;
        PasswordHash = passwordHash;
        Direccion = direccion;
        Tipo = tipo;
        ImageProfile = "";
        
        Armario = new List<string>();
        Favoritos = new List<string>();
        Publicados = new List<string>();
        Vendidos = new List<string>();
    }
    
    public Profile(SignUpCommand command, string hashedPasswordHash)
    {
        Nombre = command.Nombre;
        Apellidos = command.Apellidos;
        Email = command.Email;
        PasswordHash = hashedPasswordHash;
        Direccion = command.Direccion;
        Tipo = command.Tipo;
        ImageProfile = "";
        
        Armario = new List<string>();
        Favoritos = new List<string>();
        Publicados = new List<string>();
        Vendidos = new List<string>();
    }
}