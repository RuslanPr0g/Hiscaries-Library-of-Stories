using HC.Application.ResultModels.Response;
using HC.Application.Users.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Users.Command;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<UserWithTokenResponse>>
{
    private readonly IUserWriteService _userService;

    public RegisterUserCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult<UserWithTokenResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.RegisterUser(request);
    }
}