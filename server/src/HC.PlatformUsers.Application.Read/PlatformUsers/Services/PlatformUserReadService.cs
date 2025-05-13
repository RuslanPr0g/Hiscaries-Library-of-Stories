using HC.Application.Read.Users.DataAccess;
using HC.Application.Read.Users.ReadModels;
using HC.Domain.UserAccounts;

namespace HC.Application.Read.Users.Services;

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
