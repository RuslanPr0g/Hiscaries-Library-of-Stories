using HC.Application.ResultModels.Response;
using HC.Application.Users.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Users.Command;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OperationResult<UserWithTokenResponse>>
{
    private readonly IUserWriteService _userService;

    public LoginUserCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult<UserWithTokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.LoginUser(request);
    }
}