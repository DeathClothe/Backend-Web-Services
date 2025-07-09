using ReWear.DeathClothe.API.IAM.Domain.Model.Commands;

namespace ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;

public partial class Profile
{
    public int Id { get; }
    
    public string Nombre { get; private set; }
    
    public string Apellidos { get; private set; }
    
    public string Email { get; private set; }
    
    public string Password { get; private set; }
    
    public string Direccion { get; private set; }
    
    public string Tipo { get; private set; }
    
    public string ImageProfile { get; private set; } = "";
    
    public List<string> Armario { get; private set; }
    
    public List<string> Favoritos { get; private set; }
    
    public List<string> Publicados { get; private set; }
    
    public List<string> Vendidos { get; private set; }
    public Profile(string nombre,
        string apellidos,
        string email,
        string password,
        string direccion,
        string tipo)
        : this(nombre, apellidos, email, password, direccion, tipo, "") { }

    public Profile( string nombre,
        string apellidos,
        string email,
        string password,
        string direccion,
        string tipo,
        string? imageProfile)
    {
        Nombre = nombre;
        Apellidos = apellidos;
        Email = email;
        Password = password;
        Direccion = direccion;
        Tipo = tipo;
        ImageProfile = imageProfile ?? ""; 
        
        Armario = new List<string>();
        Favoritos = new List<string>();
        Publicados = new List<string>();
        Vendidos = new List<string>();
    }
    
    public Profile(CreateProfileCommand command)
        : this(command.Nombre, command.Apellidos, command.Email, command.Password, command.Direccion, command.Tipo)
    { }

    public void UpdateFromCommand(UpdateProfileCommand command)
    {
        Nombre = command.Nombre;
        Apellidos = command.Apellidos;
        Email = command.Email;
        Password = command.Password;
        Direccion = command.Direccion;
        Tipo = command.Tipo;
        ImageProfile = command.ImageProfile;
    }
}