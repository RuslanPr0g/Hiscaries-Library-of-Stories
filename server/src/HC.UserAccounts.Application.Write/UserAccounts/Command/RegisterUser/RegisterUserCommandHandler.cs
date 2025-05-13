using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Users.Services;
using MediatR;

namespace HC.Application.Write.UserAccounts.Command.CreateUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<TokenMetadata>>
{
    private readonly IUserAccountWriteService _userService;

    public RegisterUserCommandHandler(IUserAccountWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult<TokenMetadata>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.RegisterUser(request);
    }
}