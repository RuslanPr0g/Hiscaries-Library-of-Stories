using HC.PlatformUsers.Application.Write.PlatformUsers.Services;

namespace HC.PlatformUsers.Application.Write.PlatformUsers.Command.EditLibrary;

public class EditLibraryCommandHandler : IRequestHandler<EditLibraryCommand, OperationResult>
{
    private readonly IPlatformUserWriteService _userService;

    public EditLibraryCommandHandler(IPlatformUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(EditLibraryCommand request, CancellationToken cancellationToken)
    {
        return await _userService.EditLibraryInfo(request);
    }
}