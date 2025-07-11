using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Infrastructure.Persistence.EFC.Configurations.Extensions;
using ReWear.DeathClothe.API.Clothes.Infrastructure.Persistence.EFC.Configurations.Extensions;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using System.Text.Json;
namespace ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Clothe> Clothes { get; set; } = null!;
    public DbSet<Profile> Users { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Bounded context IAM configuration
        modelBuilder.Entity<Profile>().HasKey(p => p.Id);
        modelBuilder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Profile>().Property(p => p.Nombre).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Apellidos).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Email).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.PasswordHash).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Direccion).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Tipo).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.ImageProfile).IsRequired();
        
        modelBuilder.Entity<Profile>()
            .Property(p => p.Armario)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            )
            .HasColumnType("LONGTEXT");

        modelBuilder.Entity<Profile>()
            .Property(p => p.Favoritos)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            )
            .HasColumnType("LONGTEXT");

        modelBuilder.Entity<Profile>()
            .Property(p => p.Publicados)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            )
            .HasColumnType("LONGTEXT");

        modelBuilder.Entity<Profile>()
            .Property(p => p.Vendidos)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)
            )
            .HasColumnType("LONGTEXT");
        
        //Bounded context Clothes configuration

        modelBuilder.ApplyClotheConfiguration();

        //Bounded context Categories configuration
        
        modelBuilder.ApplyCategoryConfiguration();
        
        modelBuilder.UseSnakeCaseNamingConvention();
    }
}
