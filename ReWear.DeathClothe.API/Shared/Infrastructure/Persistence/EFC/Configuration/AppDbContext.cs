using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
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
        modelBuilder.Entity<Profile>().Property(p => p.Password).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Direccion).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Tipo).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.ImageProfile).IsRequired();

        modelBuilder.Entity<Profile>()
            .Property(p => p.Armario)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        modelBuilder.Entity<Profile>()
            .Property(p => p.Favoritos)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        modelBuilder.Entity<Profile>()
            .Property(p => p.Publicados)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        modelBuilder.Entity<Profile>()
            .Property(p => p.Vendidos)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );
        
        //Bounded context Clothes configuration

        modelBuilder.Entity<Clothe>().HasKey(c => c.Id);
        modelBuilder.Entity<Clothe>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Clothe>().Property(c => c.Nombre).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Descripcion).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Precio).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Tipo).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Talla).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Color).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Usuario).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Imagen).IsRequired();
        
        modelBuilder.Entity<Clothe>()
            .Property(c => c.Categorias)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
            );

        //Bounded context Categories configuration
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Category>().Property(c => c.Nombre).IsRequired();
        modelBuilder.Entity<Category>().Property(c => c.Imagen).IsRequired();
        modelBuilder.Entity<Category>().Property(c => c.CreatedDate);
        modelBuilder.Entity<Category>().Property(c => c.UpdatedDate);
        
        modelBuilder.UseSnakeCaseNamingConvention();
    }
}