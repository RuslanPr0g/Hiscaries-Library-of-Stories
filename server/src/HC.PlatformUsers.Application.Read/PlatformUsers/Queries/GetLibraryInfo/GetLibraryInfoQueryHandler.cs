using HC.PlatformUsers.Application.Read.PlatformUsers.ReadModels;
using HC.PlatformUsers.Application.Read.PlatformUsers.Services;

namespace HC.PlatformUsers.Application.Read.PlatformUsers.Queries.GetLibraryInfo;

public class GetLibraryInfoQueryHandler : IRequestHandler<GetLibraryInfoQuery, LibraryReadModel?>
{
    private readonly IPlatformUserReadService _userService;

    public GetLibraryInfoQueryHandler(IPlatformUserReadService userService)
    {
        _userService = userService;
    }

    public async Task<LibraryReadModel?> Handle(GetLibraryInfoQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetLibraryInformation(request.UserId, request.LibraryId);
    }
}