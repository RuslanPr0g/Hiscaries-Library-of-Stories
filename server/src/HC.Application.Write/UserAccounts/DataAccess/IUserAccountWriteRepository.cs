using HC.Domain.Notifications;

namespace HC.Application.Write.UserAccounts.DataAccess;

public interface IUserAccountWriteRepository
{
    Task<UserAccount?> GetById(UserAccountId userId);
    Task<UserAccount?> GetByUsername(string username);
    Task<bool> IsExistByEmail(string email);
    Task Add(UserAccount user);
}