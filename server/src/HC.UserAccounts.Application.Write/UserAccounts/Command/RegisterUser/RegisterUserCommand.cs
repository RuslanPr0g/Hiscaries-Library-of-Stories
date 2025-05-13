namespace HC.Application.Write.UserAccounts.Command.CreateUser;

public class RegisterUserCommand : IRequest<OperationResult<TokenMetadata>>
{
    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime BirthDate { get; set; }
}