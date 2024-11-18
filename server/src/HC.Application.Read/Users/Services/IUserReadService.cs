using HC.Application.Read.Users.ReadModels;
using HC.Domain.Users;

namespace HC.Application.Read.Users.Services;

public interface IUserReadService
{
    Task<PlatformUserReadModel?> GetUserById(UserId userId);
    Task<UserSimpleReadModel?> GetUserByUsername(string username);
}