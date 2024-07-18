using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Application.ResultModels.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Users.Command.CreateUser;

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