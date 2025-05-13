using HC.Application.Read.Users.ReadModels;

namespace HC.Application.Read.Users.Queries;

public sealed class GetUserInfoQuery : IRequest<PlatformUserReadModel?>
{
    public Guid Id { get; set; }
}