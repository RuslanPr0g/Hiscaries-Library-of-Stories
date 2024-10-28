using HC.Application.Users.ReadModels;
using System;
using System.Threading.Tasks;

namespace HC.Application.Users.DataAccess;

public interface IUserReadRepository
{
    Task<UserAccountOwnerReadModel?> GetUserById(Guid userId);
    Task<UserSimpleReadModel?> GetUserByUsername(string username);
}