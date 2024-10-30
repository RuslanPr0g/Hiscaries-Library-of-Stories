using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Users.Services;
using MediatR;

namespace HC.Application.Write.Users.Command;

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