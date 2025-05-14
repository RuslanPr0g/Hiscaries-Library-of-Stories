using HC.PlatformUsers.Application.Write.PlatformUsers.Services;

namespace HC.PlatformUsers.Application.Write.PlatformUsers.Command.SubscribeToLibrary;

public class SubscribeToLibraryCommandHandler : IRequestHandler<SubscribeToLibraryCommand, OperationResult>
{
    private readonly IPlatformUserWriteService _userService;

    public SubscribeToLibraryCommandHandler(IPlatformUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(SubscribeToLibraryCommand request, CancellationToken cancellationToken)
    {
        return await _userService.SubscribeToLibrary(request);
    }
}