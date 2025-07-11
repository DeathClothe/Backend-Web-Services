using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;

namespace ReWear.DeathClothe.API.Clothes.Infrastructure.Persistence.EFC.Configurations.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyClotheConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clothe>().HasKey(c => c.Id);

        modelBuilder.Entity<Clothe>().Property(c => c.Id)
            .IsRequired().ValueGeneratedNever();;

        modelBuilder.Entity<Clothe>().Property(c => c.Nombre).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Descripcion).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Precio).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Tipo).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Usuario).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.Imagen).IsRequired();
        modelBuilder.Entity<Clothe>().Property(c => c.ApiId).IsRequired();

        modelBuilder.Entity<Clothe>().OwnsOne(c => c.Talla, t =>
        {
            t.WithOwner().HasForeignKey("Id");
            t.Property(p => p.Value)
                .HasColumnName("Talla")
                .IsRequired();
        });

        modelBuilder.Entity<Clothe>().OwnsOne(c => c.Color, c =>
        {
            c.WithOwner().HasForeignKey("Id");
            c.Property(p => p.Value)
                .HasColumnName("Color")
                .IsRequired();
        });

        modelBuilder.Entity<Clothe>()
            .Property(c => c.Categorias)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>()
            )
            .HasColumnType("LONGTEXT");



    }
}