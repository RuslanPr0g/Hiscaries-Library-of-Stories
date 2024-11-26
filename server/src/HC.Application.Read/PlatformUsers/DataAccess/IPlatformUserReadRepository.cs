using HC.Application.Read.Users.ReadModels;
using HC.Domain.Notifications;

namespace HC.Application.Read.Users.DataAccess;

public interface IPlatformUserReadRepository
{
    Task<PlatformUserReadModel?> GetUserById(PlatformUserId userId);
    Task<PlatformUserReadModel?> GetPlatformUserByAccountUserId(UserAccountId userId);
    Task<PlatformUserId?> GetPlatformUserIdByUserAccountId(UserAccountId userId);
    Task<LibraryReadModel?> GetLibraryInformation(UserAccountId requesterId, LibraryId? libraryId);
}