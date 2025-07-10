using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;

namespace ReWear.DeathClothe.API.IAM.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile, int>
{
    bool ExistsByEmail(string email);
    
    Task<Profile?> FindByEmailAsync(string email);
    
    Task UpdateAsync(Profile profile);
}