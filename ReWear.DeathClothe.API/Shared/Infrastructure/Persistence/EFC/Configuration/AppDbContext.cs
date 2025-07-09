using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Infrastructure.Persistence.EFC.Configurations.Extensions;
using ReWear.DeathClothe.API.Clothes.Infrastructure.Persistence.EFC.Configurations.Extensions;
using ReWear.DeathClothe.API.IAM.Infrastructure.Persistence.EFC.Configurations.Extensions;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

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
        modelBuilder.ApplyUsersConfiguration();
        //Bounded context Clothes configuration

        modelBuilder.ApplyClotheConfiguration();

        //Bounded context Categories configuration
       modelBuilder.ApplyCategoryConfiguration();
        
        modelBuilder.UseSnakeCaseNamingConvention();
    }
}
