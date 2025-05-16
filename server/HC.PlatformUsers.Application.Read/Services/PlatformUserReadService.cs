using HC.PlatformUsers.Domain.DataAccess;
using HC.PlatformUsers.Domain.ReadModels;
using HC.PlatformUsers.Domain.Services;

namespace HC.PlatformUsers.Application.Read.Services;

public sealed class PlatformUserReadService(IPlatformUserReadRepository repository) : IPlatformUserReadService
{
    private readonly IPlatformUserReadRepository _repository = repository;

    public async Task<PlatformUserReadModel?> GetUserById(Guid userAccountId) =>
        await _repository.GetPlatformUserByAccountUserId(userAccountId);

    public async Task<LibraryReadModel?> GetLibraryInformation(Guid requesterUserAccountId, LibraryId? libraryId = null) =>
        await _repository.GetLibraryInformation(requesterUserAccountId, libraryId);
}
