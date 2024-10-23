using MediatR;

namespace HC.Application.Users.Query;

public sealed class GetUserInfoQuery : IRequest<UserSimpleReadModel?>
{
    public string Username { get; set; }
}