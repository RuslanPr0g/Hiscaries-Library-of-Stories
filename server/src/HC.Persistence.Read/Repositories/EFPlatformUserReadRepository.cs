using HC.Application.Read.Users.DataAccess;
using HC.Application.Read.Users.ReadModels;
using HC.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Persistence.Read.Repositories;

public class EFPlatformUserReadRepository : IPlatformUserReadRepository
{
    private readonly HiscaryContext _context;

    public EFPlatformUserReadRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<PlatformUserReadModel?> GetUserById(Guid userId) =>
        await _context.PlatformUsers
            .AsNoTracking()
            .Where(x => x.Id.Value == userId)
            .Select(user => PlatformUserReadModel.FromDomainModel(user))
            .FirstOrDefaultAsync();
}

