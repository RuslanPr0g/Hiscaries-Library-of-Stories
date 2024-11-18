using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.UserAccounts.Command.LoginUser;

public class LoginUserCommand : IRequest<OperationResult<UserWithTokenResponse>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}