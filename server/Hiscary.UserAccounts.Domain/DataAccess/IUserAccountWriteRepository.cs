using StackNucleus.DDD.Domain.Repositories;

namespace Hiscary.UserAccounts.Domain.DataAccess;

public interface IUserAccountWriteRepository : IBaseWriteRepository<UserAccount, UserAccountId>
{
    Task<UserAccount?> GetByUsername(string username);
    Task<bool> IsExistByEmail(string email);
}