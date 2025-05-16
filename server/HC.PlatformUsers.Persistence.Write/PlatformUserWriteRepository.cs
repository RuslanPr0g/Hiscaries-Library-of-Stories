using HC.PlatformUsers.Domain;
using HC.PlatformUsers.Domain.DataAccess;
using HC.PlatformUsers.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.PlatformUsers.Persistence.Write;

public class PlatformUserWriteRepository(PlatformUsersContext context) :
    BaseWriteRepository<PlatformUser, PlatformUserId, PlatformUsersContext>,
    IPlatformUserWriteRepository
{
    protected override PlatformUsersContext Context { get; init; } = context;

    public async Task<List<Guid>> GetLibrarySubscribersUserAccountIds(LibraryId libraryId) =>
        await Context.PlatformUsers
        .Include(x => x.Subscriptions)
        .Where(x => x.Subscriptions.Any(y => y.LibraryId == libraryId))
        .Select(x => x.UserAccountId)
        .ToListAsync();

    public async Task<PlatformUser?> GetLibraryOwnerByLibraryId(LibraryId libraryId) =>
        await Context.PlatformUsers
        .Include(x => x.ReadHistory)
        .Include(x => x.Libraries)
        .Include(x => x.Bookmarks)
        .Include(x => x.Reviews)
        .Include(x => x.Subscriptions)
        .FirstOrDefaultAsync(x => x.Libraries.Any(x => x.Id == libraryId));

    public async Task<PlatformUser?> GetByUserAccountId(Guid userAccountId) =>
        await Context.PlatformUsers
        .Include(x => x.ReadHistory)
        .Include(x => x.Libraries)
        .Include(x => x.Bookmarks)
        .Include(x => x.Reviews)
        .Include(x => x.Subscriptions)
        .FirstOrDefaultAsync(x => x.UserAccountId == userAccountId);
}
