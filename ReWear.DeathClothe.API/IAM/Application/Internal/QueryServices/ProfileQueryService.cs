using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Queries;
using ReWear.DeathClothe.API.IAM.Domain.Repositories;
using ReWear.DeathClothe.API.IAM.Domain.Services;

namespace ReWear.DeathClothe.API.IAM.Application.Internal.QueryServices;

public class ProfileQueryService(
    IProfileRepository profileRepository
    ) : IProfileQueryService
{
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }
}
