using HC.Application.Write.Users.Services;

namespace HC.Application.Write.UserAccounts.Command.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, OperationResult<TokenMetadata>>
{
    private readonly IUserAccountWriteService _userService;

    public RefreshTokenCommandHandler(IUserAccountWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult<TokenMetadata>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _userService.RefreshToken(request);
    }
}