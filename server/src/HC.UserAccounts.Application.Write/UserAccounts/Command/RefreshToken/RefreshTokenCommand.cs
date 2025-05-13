using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.UserAccounts.Command.RefreshToken;

public class RefreshTokenCommand : IRequest<OperationResult<TokenMetadata>>
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}