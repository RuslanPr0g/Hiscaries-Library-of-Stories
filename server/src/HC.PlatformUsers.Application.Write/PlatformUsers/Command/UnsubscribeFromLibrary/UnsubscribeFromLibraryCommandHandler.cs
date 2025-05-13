using HC.Application.Write.PlatformUsers.Services;

namespace HC.Application.Write.PlatformUsers.Command.PublishReview;

public class UnsubscribeFromLibraryCommandHandler : IRequestHandler<UnsubscribeFromLibraryCommand, OperationResult>
{
    private readonly IPlatformUserWriteService _userService;

    public UnsubscribeFromLibraryCommandHandler(IPlatformUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(UnsubscribeFromLibraryCommand request, CancellationToken cancellationToken)
    {
        return await _userService.UnsubscribeFromLibrary(request);
    }
}