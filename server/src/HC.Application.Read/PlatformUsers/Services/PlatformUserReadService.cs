using HC.Application.Read.Users.DataAccess;
using HC.Application.Read.Users.ReadModels;

namespace HC.Application.Read.Users.Services;

public sealed class PlatformUserReadService : IPlatformUserReadService
{
    private readonly IPlatformUserReadRepository _repository;

    public PlatformUserReadService(IPlatformUserReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<PlatformUserReadModel?> GetUserById(PlatformUserId userId) => await _repository.GetUserById(userId);
}
