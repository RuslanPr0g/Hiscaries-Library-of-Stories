using Enterprise.Domain.DataAccess;
using HC.PlatformUsers.Domain.ProcessModels;
using HC.PlatformUsers.Domain.ReadModels;

namespace HC.PlatformUsers.Domain.DataAccess;

public interface IPlatformUserReadRepository : IBaseReadRepository<PlatformUserReadModel>
{
    Task<PlatformUserReadModel?> GetUserById(PlatformUserId userId);
    Task<PlatformUserReadModel?> GetPlatformUserByAccountUserId(Guid userAccountId);
    Task<PlatformUserId?> GetPlatformUserIdByUserAccountId(Guid userAccountId);
    Task<LibraryReadModel?> GetLibraryInformation(Guid requesterUserAccountId, LibraryId? libraryId);
    Task<IEnumerable<UserReadingStoryMetadataReadModel>> GetUserReadingStoryMetadataInformation(
        Guid requesterUserAccountId,
        UserReadingStoryProcessModel[] stories);
}