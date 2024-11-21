using HC.Application.Write.PlatformUsers.Services;
using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.PlatformUsers.Command.PublishReview;

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