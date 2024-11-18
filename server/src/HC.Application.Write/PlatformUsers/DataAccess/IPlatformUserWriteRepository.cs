using HC.Domain.PlatformUsers;
using HC.Domain.UserAccounts;

namespace HC.Application.Write.PlatformUsers.DataAccess;

public interface IPlatformUserWriteRepository
{
    Task<PlatformUser?> GetById(PlatformUserId userId);
    Task<PlatformUser?> GetByUserAccountId(UserAccountId userId);
    Task Add(PlatformUser user);
}
