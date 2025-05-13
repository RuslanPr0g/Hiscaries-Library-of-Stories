using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Users.Services;
using MediatR;

namespace HC.Application.Write.UserAccounts.Command.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OperationResult<TokenMetadata>>
{
    private readonly IUserAccountWriteService _userService;

    public LoginUserCommandHandler(IUserAccountWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult<TokenMetadata>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.LoginUser(request);
    }
}