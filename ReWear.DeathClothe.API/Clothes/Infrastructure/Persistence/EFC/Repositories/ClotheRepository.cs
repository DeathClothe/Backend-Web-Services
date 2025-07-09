using Microsoft.EntityFrameworkCore;
using ReWear.DeathClothe.API.Clothes.Domain.Model.Aggregates;
using ReWear.DeathClothe.API.Clothes.Domain.Repositories;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace ReWear.DeathClothe.API.Clothes.Infrastructure.Persistence.EFC.Repositories;

public class ClotheRepository : BaseRepository<Clothe, string>, IClotheRepository
{
    private readonly AppDbContext _context;

    public ClotheRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<string?> GetLastClotheIdAsync()
    {
        var last = await _context.Clothes
            .Where(c => EF.Functions.Like(c.Id, "P%"))
            .OrderByDescending(c => c.Id)
            .FirstOrDefaultAsync();

        Console.WriteLine($"✅ Último ID encontrado: {last?.Id}");
        return last?.Id;
    }


}