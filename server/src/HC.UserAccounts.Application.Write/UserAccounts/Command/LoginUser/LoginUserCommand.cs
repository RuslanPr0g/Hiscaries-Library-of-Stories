namespace HC.UserAccounts.Application.Write.UserAccounts.Command.LoginUser;

public class LoginUserCommand : IRequest<OperationResult<TokenMetadata>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}