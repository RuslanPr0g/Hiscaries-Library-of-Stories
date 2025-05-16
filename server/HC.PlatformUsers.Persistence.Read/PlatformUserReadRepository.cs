using HC.PlatformUsers.Domain.DataAccess;
using HC.PlatformUsers.Domain.ReadModels;
using HC.PlatformUsers.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.PlatformUsers.Persistence.Read;

public class PlatformUserReadRepository(PlatformUsersContext context) :
    BaseReadRepository<PlatformUserReadModel>,
    IPlatformUserReadRepository
{
    private PlatformUsersContext Context { get; init; } = context;


    public async Task<PlatformUserReadModel?> GetUserById(PlatformUserId userId) =>
        await Context.PlatformUsers
            .AsNoTracking()
            .Where(x => x.UserAccountId == userId.Value)
            .Select(user => PlatformUserReadModel.FromDomainModel(user))
            .FirstOrDefaultAsync();

    public async Task<PlatformUserReadModel?> GetPlatformUserByAccountUserId(Guid userId) =>
        await Context.PlatformUsers
            .AsNoTracking()
            .Where(x => x.UserAccountId == userId)
            .Select(user => PlatformUserReadModel.FromDomainModel(user))
            .FirstOrDefaultAsync();

    public async Task<PlatformUserId?> GetPlatformUserIdByUserAccountId(Guid userId) =>
        await Context.PlatformUsers
            .AsNoTracking()
            .Where(x => x.UserAccountId == userId)
            .Select(user => user.Id)
            .FirstOrDefaultAsync();

    public async Task<LibraryReadModel?> GetLibraryInformation(Guid requesterId, LibraryId? libraryId)
    {
        var isUserSubscribedToLibrary = libraryId is not null && await IsUserSubscribedToLibrary(requesterId, libraryId);

        if (libraryId is null)
        {
            return await Context.Libraries
                .AsNoTracking()
                .Include(x => x.PlatformUser)
                .Where(x => x.PlatformUser.UserAccountId == requesterId)
                .Select(x => LibraryReadModel.FromDomainModel(x, requesterId, isUserSubscribedToLibrary))
                .FirstOrDefaultAsync();
        }

        return await Context.Libraries
            .AsNoTracking()
            .Include(x => x.PlatformUser)
            .Where(x => x.Id == libraryId)
            .Select(x => LibraryReadModel.FromDomainModel(x, requesterId, isUserSubscribedToLibrary))
            .FirstOrDefaultAsync();
    }

    private async Task<bool> IsUserSubscribedToLibrary(Guid requesterId, LibraryId libraryId)
    {
        return await Context.PlatformUserToLibrarySubscriptions
            .Include(x => x.PlatformUser)
            .AnyAsync(x => x.LibraryId == libraryId && x.PlatformUser.UserAccountId == requesterId);
    }
}
