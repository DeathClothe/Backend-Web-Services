using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Queries;
using ReWear.DeathClothe.API.IAM.Domain.Repositories;
using ReWear.DeathClothe.API.IAM.Domain.Services;

namespace ReWear.DeathClothe.API.IAM.Application.Internal.QueryServices;

public class ProfileQueryService(IProfileRepository repository)
    : IProfileQueryService
{
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await repository.FindByIdAsync(query.Id);
    }

    public async Task<Profile?> Handle(GetProfileByEmailQuery query)
    {
        return await repository.FindProfileByEmailAsync(query.Email);
    }

    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await repository.ListAsync();
    }
}