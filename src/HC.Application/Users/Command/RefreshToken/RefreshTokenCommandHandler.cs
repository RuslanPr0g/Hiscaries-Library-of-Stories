using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Models.Response;
using MediatR;

namespace HC.Application.Users.Command.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IUserWriteService _userService;

    public RefreshTokenCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _userService.RefreshToken(request.Token, request.RefreshToken, request.User);
    }
}