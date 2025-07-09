using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;

public  partial class Clothe : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdateAt")]  public DateTimeOffset? UpdatedDate { get; set; }
}