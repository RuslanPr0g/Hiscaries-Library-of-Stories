using HC.Application.Models.Response;
using HC.Application.ResultModels.Response;
using MediatR;

namespace HC.Application.Users.Command.RefreshToken;

public class RefreshTokenCommand : IRequest<OperationResult<UserWithTokenResponse>>
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}