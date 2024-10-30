using HC.Domain.Users;

namespace HC.Application.Write.Users.DataAccess;

public interface IUserWriteRepository
{
    Task<User?> GetUserById(UserId userId);
    Task<User?> GetUserByUsername(string username);
    Task<bool> IsUserExistByEmail(string email);
    Task AddUser(User user);
}