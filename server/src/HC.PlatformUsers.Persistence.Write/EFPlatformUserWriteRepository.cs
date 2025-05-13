using HC.Application.Write.PlatformUsers.DataAccess;
using HC.Domain.PlatformUsers;
using HC.Domain.UserAccounts;
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

    public async Task<IEnumerable<UserAccountId>> GetLibrarySubscribersUserAccountIds(LibraryId libraryId) =>
        await _context.PlatformUsers
        .Include(x => x.Subscriptions)
        .Where(x => x.Subscriptions.Any(y => y.LibraryId == libraryId))
        .Select(x => x.UserAccountId)
        .ToListAsync();

    public async Task<PlatformUser?> GetById(PlatformUserId userId) =>
        await _context.PlatformUsers
        .Include(x => x.ReadHistory)
        .Include(x => x.Libraries)
        .Include(x => x.Bookmarks)
        .Include(x => x.Reviews)
        .Include(x => x.Subscriptions)
        .FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<PlatformUser?> GetLibraryOwnerByLibraryId(LibraryId libraryId) =>
        await _context.PlatformUsers
        .Include(x => x.ReadHistory)
        .Include(x => x.Libraries)
        .Include(x => x.Bookmarks)
        .Include(x => x.Reviews)
        .Include(x => x.Subscriptions)
        .FirstOrDefaultAsync(x => x.Libraries.Any(x => x.Id == libraryId));

    public async Task<PlatformUser?> GetByUserAccountId(UserAccountId userId) =>
        await _context.PlatformUsers
        .Include(x => x.ReadHistory)
        .Include(x => x.Libraries)
        .Include(x => x.Bookmarks)
        .Include(x => x.Reviews)
        .Include(x => x.Subscriptions)
        .FirstOrDefaultAsync(x => x.UserAccountId == userId);

    public async Task Add(PlatformUser user) =>
        await _context.PlatformUsers.AddAsync(user);
}

