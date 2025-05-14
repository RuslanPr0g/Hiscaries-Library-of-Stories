using HC.PlatformUsers.Domain.ReadModels;

namespace HC.PlatformUsers.Domain.DataAccess;

public interface IPlatformUserReadRepository
{
    Task<PlatformUserReadModel?> GetUserById(PlatformUserId userId);
    Task<PlatformUserReadModel?> GetPlatformUserByAccountUserId(Guid userAccountId);
    Task<PlatformUserId?> GetPlatformUserIdByUserAccountId(Guid userAccountId);
    Task<LibraryReadModel?> GetLibraryInformation(Guid requesterUserAccountId, LibraryId? libraryId);
}