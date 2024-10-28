using HC.Application.Users.ReadModels;
using HC.Application.Users.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Users.Query;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserSimpleReadModel?>
{
    private readonly IUserReadService _userService;

    public GetUserInfoQueryHandler(IUserReadService userService)
    {
        _userService = userService;
    }

    public async Task<UserSimpleReadModel?> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUserByUsername(request.Username);
    }
}