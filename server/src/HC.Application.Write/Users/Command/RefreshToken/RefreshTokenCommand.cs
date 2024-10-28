using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.Users.Command;

public class RefreshTokenCommand : IRequest<OperationResult<UserWithTokenResponse>>
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}