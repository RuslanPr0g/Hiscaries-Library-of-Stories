using HC.Domain.PlatformUsers;

namespace HC.Application.Write.PlatformUsers.DataAccess;

public interface IPlatformUserWriteRepository
{
    Task<PlatformUser?> GetById(PlatformUserId userId);
    Task Add(PlatformUser user);
}
