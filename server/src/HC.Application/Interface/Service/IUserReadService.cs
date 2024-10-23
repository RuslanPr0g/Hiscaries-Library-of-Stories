using HC.Domain.Users;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IUserReadService
{
    Task<UserAccountOwnerReadModel?> GetUserById(UserId userId);
    Task<UserSimpleReadModel?> GetUserByUsername(string username);
}