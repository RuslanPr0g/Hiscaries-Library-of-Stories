namespace HC.UserAccounts.Application.Write.Services.RefreshToken;

public class RefreshTokenCommand : IRequest<OperationResult<TokenMetadata>>
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}