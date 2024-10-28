using HC.Application.Read.Users.ReadModels;

namespace HC.Application.Read.Users.DataAccess;

public interface IUserReadRepository
{
    Task<UserAccountOwnerReadModel?> GetUserById(Guid userId);
    Task<UserSimpleReadModel?> GetUserByUsername(string username);
}