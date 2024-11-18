using HC.Application.Read.Users.ReadModels;

namespace HC.Application.Read.Users.DataAccess;

public interface IPlatformUserReadRepository
{
    Task<PlatformUserReadModel?> GetUserById(Guid userId);
}