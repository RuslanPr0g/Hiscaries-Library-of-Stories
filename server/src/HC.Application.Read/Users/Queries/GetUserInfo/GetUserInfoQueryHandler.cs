using HC.Application.Read.Users.ReadModels;
using HC.Application.Read.Users.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Read.Users.Queries;

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