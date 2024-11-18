using HC.Application.Read.Users.ReadModels;

namespace HC.Application.Read.Users.Services;

public interface IPlatformUserReadService
{
    Task<PlatformUserReadModel?> GetUserById(PlatformUserId userId);
}