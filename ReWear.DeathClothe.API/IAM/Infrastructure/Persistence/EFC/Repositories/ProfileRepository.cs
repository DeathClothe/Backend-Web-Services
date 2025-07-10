using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace ReWear.DeathClothe.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository : BaseRepository<Profile, int>, IProfileRepository
{
    public ProfileRepository(AppDbContext context) : base(context) { }

    public bool ExistsByEmail(string email)
    {
        return Context.Set<Profile>().Any(profile => profile.Email.Equals(email));
    }

    public async Task<Profile?> FindByEmailAsync(string email)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(profile => profile.Email.Equals(email));
    }
    
    public async Task UpdateAsync(Profile profile)
    {
        Context.Set<Profile>().Update(profile);
        await Context.SaveChangesAsync();
    }
}