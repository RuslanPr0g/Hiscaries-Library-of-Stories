using HC.Domain.Users;
using System.Threading.Tasks;

namespace HC.Application.DataAccess;

public interface IUserWriteRepository
{
    Task<User?> GetUserById(UserId userId);
    Task<User?> GetUserByUsername(string username);
    Task<bool> IsUserExistByEmail(string email);
    Task AddUser(User user);
}