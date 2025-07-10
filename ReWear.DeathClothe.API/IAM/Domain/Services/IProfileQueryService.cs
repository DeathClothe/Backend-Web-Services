using ReWear.DeathClothe.API.IAM.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.IAM.Domain.Model.Queries;

namespace ReWear.DeathClothe.API.IAM.Domain.Services;

public interface IProfileQueryService
{
    Task<Profile?> Handle(GetProfileByIdQuery query);

    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
}