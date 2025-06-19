using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
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
        
        modelBuilder.UseSnakeCaseNamingConvention();
    }
}