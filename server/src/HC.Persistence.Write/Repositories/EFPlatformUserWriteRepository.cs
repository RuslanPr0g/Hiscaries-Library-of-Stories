using HC.Application.Write.Users.DataAccess;
using HC.Domain.PlatformUsers;
using HC.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Persistence.Write.Repositories;

public class EFPlatformUserWriteRepository : IPlatformUserWriteRepository
{
    private readonly HiscaryContext _context;

    public EFPlatformUserWriteRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<PlatformUser?> GetById(PlatformUserId userId) =>
        await _context.PlatformUsers.FirstOrDefaultAsync(x => x.Id == userId);

    public async Task Add(PlatformUser user) =>
        await _context.PlatformUsers.AddAsync(user);
}

