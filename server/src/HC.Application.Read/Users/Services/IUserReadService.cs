using HC.Application.Read.Users.ReadModels;
using HC.Domain.Users;
using System.Threading.Tasks;

namespace HC.Application.Read.Users.Services;

public interface IUserReadService
{
    Task<UserAccountOwnerReadModel?> GetUserById(UserId userId);
    Task<UserSimpleReadModel?> GetUserByUsername(string username);
}