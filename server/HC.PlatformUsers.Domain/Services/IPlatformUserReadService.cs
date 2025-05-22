using HC.PlatformUsers.Domain.ProcessModels;
using HC.PlatformUsers.Domain.ReadModels;

namespace HC.PlatformUsers.Domain.Services;

public interface IPlatformUserReadService
{
    Task<PlatformUserReadModel?> GetUserById(Guid userAccountId);
    Task<LibraryReadModel?> GetLibraryInformation(Guid requesterId, LibraryId? libraryId = null);

    Task<IEnumerable<UserReadingStoryMetadataReadModel>> GetUserReadingStoryMetadata(
        Guid requesterUserAccountId,
        UserReadingStoryProcessModel[] stories);
}