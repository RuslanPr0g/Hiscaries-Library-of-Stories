using HC.PlatformUsers.Application.Read.PlatformUsers.ReadModels;

namespace HC.PlatformUsers.Application.Read.PlatformUsers.Queries.GetUserInfo;

public sealed class GetUserInfoQuery : IRequest<PlatformUserReadModel?>
{
    public Guid Id { get; set; }
}