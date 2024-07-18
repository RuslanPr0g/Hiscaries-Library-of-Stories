using HC.Application.Models.Response;
using HC.Application.ResultModels.Response;
using MediatR;

namespace HC.Application.Users.Command.LoginUser;

public class LoginUserCommand : IRequest<OperationResult<UserWithTokenResponse>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}