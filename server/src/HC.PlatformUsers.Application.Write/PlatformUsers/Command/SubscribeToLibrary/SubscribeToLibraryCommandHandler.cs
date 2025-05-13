using HC.Application.Write.PlatformUsers.Services;
using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.PlatformUsers.Command.PublishReview;

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