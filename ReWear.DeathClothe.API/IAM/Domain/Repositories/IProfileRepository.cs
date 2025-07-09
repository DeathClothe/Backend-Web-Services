using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Shared.Domain.Repositories;

namespace ReWear.DeathClothe.API.IAM.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile, int>
{
    Task<Profile?> FindProfileByEmailAsync(string email);
}