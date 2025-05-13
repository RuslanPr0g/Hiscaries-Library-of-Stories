using HC.PlatformUsers.Application.Read.PlatformUsers.ReadModels;
using HC.PlatformUsers.Application.Read.PlatformUsers.Services;

namespace HC.PlatformUsers.Application.Read.PlatformUsers.Queries.GetUserInfo;

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