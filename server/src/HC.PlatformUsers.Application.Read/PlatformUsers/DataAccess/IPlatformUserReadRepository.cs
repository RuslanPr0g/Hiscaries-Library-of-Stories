using HC.PlatformUsers.Application.Read.PlatformUsers.ReadModels;

namespace HC.PlatformUsers.Application.Read.PlatformUsers.DataAccess;

public interface IPlatformUserReadRepository
{
    Task<PlatformUserReadModel?> GetUserById(PlatformUserId userId);
    Task<PlatformUserReadModel?> GetPlatformUserByAccountUserId(UserAccountId userId);
    Task<PlatformUserId?> GetPlatformUserIdByUserAccountId(UserAccountId userId);
    Task<LibraryReadModel?> GetLibraryInformation(UserAccountId requesterId, LibraryId? libraryId);
}