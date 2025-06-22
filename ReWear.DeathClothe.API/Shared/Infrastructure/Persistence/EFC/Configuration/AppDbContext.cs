using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
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
        
        //Bounded context Clothes configuration
        
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