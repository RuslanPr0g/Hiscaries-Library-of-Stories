using HC.Application.Read.Users.ReadModels;
using HC.Domain.UserAccounts;

namespace HC.Application.Read.Users.DataAccess;

public interface IPlatformUserReadRepository
{
    Task<PlatformUserReadModel?> GetUserById(Guid userId);
    Task<PlatformUserId?> GetPlatformUserIdByUserAccountId(UserAccountId userId);
}