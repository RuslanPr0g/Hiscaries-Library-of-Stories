using System;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IUserReadRepository
{
    Task<UserAccountOwnerReadModel?> GetUserById(Guid userId);
    Task<UserAccountOwnerReadModel?> GetUserByUsername(string username);
}