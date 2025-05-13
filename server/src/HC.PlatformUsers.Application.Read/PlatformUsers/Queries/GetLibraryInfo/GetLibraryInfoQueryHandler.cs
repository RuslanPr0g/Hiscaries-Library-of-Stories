using HC.Application.Read.Users.ReadModels;
using HC.Application.Read.Users.Services;

namespace HC.Application.Read.Users.Queries;

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