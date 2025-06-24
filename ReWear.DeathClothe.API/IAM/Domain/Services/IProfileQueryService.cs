using ReWear.DeathClothe.API.IAM.Domain.Model.Queries;

namespace ReWear.DeathClothe.API.IAM.Domain.Services;

using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;

public interface IProfileQueryService
{
    Task<Profile?> Handle(GetProfileByIdQuery query);
    
    Task<Profile?> Handle(GetProfileByEmailQuery query);

    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
}