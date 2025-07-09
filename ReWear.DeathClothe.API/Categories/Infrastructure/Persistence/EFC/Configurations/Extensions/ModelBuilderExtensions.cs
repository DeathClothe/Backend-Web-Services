using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;

namespace ReWear.DeathClothe.API.Categories.Infrastructure.Persistence.EFC.Configurations.Extensions;

public static class  ModelBuilderExtensions
{
    public static void ApplyCategoryConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Category>().Property(c => c.Nombre).IsRequired();
        modelBuilder.Entity<Category>().Property(c => c.Imagen).IsRequired();
        modelBuilder.Entity<Category>().Property(c => c.CreatedDate);
        modelBuilder.Entity<Category>().Property(c => c.UpdatedDate);
    }

}