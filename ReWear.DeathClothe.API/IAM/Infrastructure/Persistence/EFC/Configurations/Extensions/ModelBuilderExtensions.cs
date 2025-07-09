using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;

namespace ReWear.DeathClothe.API.IAM.Infrastructure.Persistence.EFC.Configurations.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyUsersConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>().HasKey(p => p.Id);
        modelBuilder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Profile>().Property(p => p.Nombre).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Apellidos).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Email).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Password).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Direccion).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.Tipo).IsRequired();
        modelBuilder.Entity<Profile>().Property(p => p.ImageProfile).IsRequired(false);

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

    }
}