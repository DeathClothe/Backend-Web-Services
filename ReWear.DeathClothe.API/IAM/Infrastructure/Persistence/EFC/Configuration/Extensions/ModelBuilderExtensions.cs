using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;

namespace ReWear.DeathClothe.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        // IAM Context
        
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Profile>().Property(p => p.Nombre).IsRequired();
        builder.Entity<Profile>().Property(p => p.Apellidos).IsRequired();
        builder.Entity<Profile>().Property(p => p.Email).IsRequired();
        builder.Entity<Profile>().Property(p => p.PasswordHash).IsRequired();
        builder.Entity<Profile>().Property(p => p.Direccion).IsRequired();
        builder.Entity<Profile>().Property(p => p.Tipo).IsRequired();
        builder.Entity<Profile>().Property(p => p.ImageProfile).IsRequired();
        
        builder.Entity<Profile>()
            .Property(p => p.Armario)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>()
            )
            .HasColumnType("LONGTEXT");

        builder.Entity<Profile>()
            .Property(p => p.Favoritos)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>()
            )
            .HasColumnType("LONGTEXT");

        builder.Entity<Profile>()
            .Property(p => p.Publicados)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>()
            )
            .HasColumnType("LONGTEXT");

        builder.Entity<Profile>()
            .Property(p => p.Vendidos)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>()
            )
            .HasColumnType("LONGTEXT");
    }
}