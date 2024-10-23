using MediatR;

namespace HC.Application.Users.Query;

public sealed class GetUserInfoQuery : IRequest<UserAccountOwnerReadModel>
{
    public string Username { get; set; }
}