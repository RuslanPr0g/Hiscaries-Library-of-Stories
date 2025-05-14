using HC.PlatformUsers.Application.Read.PlatformUsers.DataAccess;
using HC.PlatformUsers.Application.Read.PlatformUsers.ReadModels;

namespace HC.PlatformUsers.Application.Read.Services;

public sealed class PlatformUserReadService : IPlatformUserReadService
{
    private readonly IPlatformUserReadRepository _repository;

    public PlatformUserReadService(IPlatformUserReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<PlatformUserReadModel?> GetUserById(UserAccountId userId) =>
        await _repository.GetPlatformUserByAccountUserId(userId);

    public async Task<LibraryReadModel?> GetLibraryInformation(UserAccountId requesterId, LibraryId? libraryId = null) =>
        await _repository.GetLibraryInformation(requesterId, libraryId);
}
