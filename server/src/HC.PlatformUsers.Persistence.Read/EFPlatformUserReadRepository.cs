using Microsoft.EntityFrameworkCore;

namespace HC.Persistence.Read.Repositories;

public class EFPlatformUserReadRepository : IPlatformUserReadRepository
{
    private readonly HiscaryContext _context;

    public EFPlatformUserReadRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<PlatformUserReadModel?> GetUserById(PlatformUserId userId) =>
        await _context.PlatformUsers
            .AsNoTracking()
            .Where(x => x.UserAccountId == userId)
            .Select(user => PlatformUserReadModel.FromDomainModel(user))
            .FirstOrDefaultAsync();

    public async Task<PlatformUserReadModel?> GetPlatformUserByAccountUserId(UserAccountId userId) =>
        await _context.PlatformUsers
            .AsNoTracking()
            .Where(x => x.UserAccountId == userId)
            .Select(user => PlatformUserReadModel.FromDomainModel(user))
            .FirstOrDefaultAsync();

    public async Task<PlatformUserId?> GetPlatformUserIdByUserAccountId(UserAccountId userId) =>
        await _context.PlatformUsers
            .AsNoTracking()
            .Where(x => x.UserAccountId == userId)
            .Select(user => user.Id)
            .FirstOrDefaultAsync();

    public async Task<LibraryReadModel?> GetLibraryInformation(UserAccountId requesterId, LibraryId? libraryId)
    {
        var isUserSubscribedToLibrary = libraryId is not null && await IsUserSubscribedToLibrary(requesterId, libraryId);

        if (libraryId is null)
        {
            return await _context.Libraries
                .AsNoTracking()
                .Include(x => x.PlatformUser)
                .Where(x => x.PlatformUser.UserAccountId == requesterId)
                .Select(x => LibraryReadModel.FromDomainModel(x, requesterId, isUserSubscribedToLibrary))
                .FirstOrDefaultAsync();
        }

        return await _context.Libraries
            .AsNoTracking()
            .Include(x => x.PlatformUser)
            .Where(x => x.Id == libraryId)
            .Select(x => LibraryReadModel.FromDomainModel(x, requesterId, isUserSubscribedToLibrary))
            .FirstOrDefaultAsync();
    }

    private async Task<bool> IsUserSubscribedToLibrary(UserAccountId requesterId, LibraryId libraryId)
    {
        return await _context.PlatformUserToLibrarySubscriptions
            .Include(x => x.PlatformUser)
            .AnyAsync(x => x.LibraryId == libraryId && x.PlatformUser.UserAccountId == requesterId);
    }
}

