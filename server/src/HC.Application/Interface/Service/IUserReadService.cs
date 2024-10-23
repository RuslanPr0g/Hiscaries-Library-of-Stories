using HC.Domain.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IUserReadService
{
    Task<UserAccountOwnerReadModel> GetUserById(UserId userId);
    Task<UserAccountOwnerReadModel> GetUserByUsername(string username);
}