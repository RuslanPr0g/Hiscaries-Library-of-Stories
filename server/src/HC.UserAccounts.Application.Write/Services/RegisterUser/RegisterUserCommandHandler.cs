namespace HC.UserAccounts.Application.Write.Services.RegisterUser;

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