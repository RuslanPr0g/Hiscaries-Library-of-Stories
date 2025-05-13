using HC.Application.Read.Users.ReadModels;
using HC.Application.Read.Users.Services;
using MediatR;

namespace HC.Application.Read.Users.Queries;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, PlatformUserReadModel?>
{
    private readonly IPlatformUserReadService _userService;

    public GetUserInfoQueryHandler(IPlatformUserReadService userService)
    {
        _userService = userService;
    }

    public async Task<PlatformUserReadModel?> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetUserById(request.Id);
    }
}