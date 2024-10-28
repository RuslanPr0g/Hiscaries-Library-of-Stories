using System;
using System.Threading.Tasks;

namespace HC.Application.DataAccess;

public interface IUserReadRepository
{
    Task<UserAccountOwnerReadModel?> GetUserById(Guid userId);
    Task<UserSimpleReadModel?> GetUserByUsername(string username);
}