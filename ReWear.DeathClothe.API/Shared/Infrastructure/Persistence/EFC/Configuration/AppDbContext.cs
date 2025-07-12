using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Categories.Infrastructure.Persistence.EFC.Configurations.Extensions;
using ReWear.DeathClothe.API.Clothes.Infrastructure.Persistence.EFC.Configurations.Extensions;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using ReWear.DeathClothe.API.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Clothe> Clothes { get; set; } = null!;
    public DbSet<Profile> Users { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //Bounded context IAM configuration
        builder.ApplyIamConfiguration();
        
        //Bounded context Clothes configuration

        builder.ApplyClotheConfiguration();

        //Bounded context Categories configuration
        
        builder.ApplyCategoryConfiguration();
        
        builder.UseSnakeCaseNamingConvention();
    }
}
