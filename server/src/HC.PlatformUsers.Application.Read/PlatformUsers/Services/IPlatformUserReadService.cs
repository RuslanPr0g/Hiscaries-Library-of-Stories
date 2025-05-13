using HC.PlatformUsers.Application.Read.PlatformUsers.ReadModels;

namespace HC.PlatformUsers.Application.Read.PlatformUsers.Services;

public interface IPlatformUserReadService
{
    Task<PlatformUserReadModel?> GetUserById(UserAccountId userId);
    Task<LibraryReadModel?> GetLibraryInformation(UserAccountId requesterId, LibraryId? libraryId = null);
}