using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace ReWear.DeathClothe.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context)
    : BaseRepository<Profile,int>(context), IProfileRepository
{
    public async Task<Profile?> FindProfileByEmailAsync(string email)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(x => x.Email == email);
    }
}