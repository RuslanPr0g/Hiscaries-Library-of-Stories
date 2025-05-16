using HC.PlatformUsers.Domain.ReadModels;

namespace HC.PlatformUsers.Domain.Services;

public interface IPlatformUserReadService
{
    Task<PlatformUserReadModel?> GetUserById(Guid userAccountId);
    Task<LibraryReadModel?> GetLibraryInformation(Guid requesterId, LibraryId? libraryId = null);
}