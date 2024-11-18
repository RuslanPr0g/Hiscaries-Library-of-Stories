using HC.Domain.PlatformUsers;

namespace HC.Application.Write.Users.DataAccess;

public interface IPlatformUserWriteRepository
{
    Task<PlatformUser?> GetById(PlatformUserId userId);
    Task Add(PlatformUser user);
}
