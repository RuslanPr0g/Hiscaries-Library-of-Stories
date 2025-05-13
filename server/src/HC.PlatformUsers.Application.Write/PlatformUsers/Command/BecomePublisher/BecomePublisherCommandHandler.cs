using HC.Application.Write.PlatformUsers.Services;

namespace HC.Application.Write.PlatformUsers.Command.BecomePublisher;

public class BecomePublisherCommandHandler : IRequestHandler<BecomePublisherCommand, OperationResult>
{
    private readonly IPlatformUserWriteService _userService;

    public BecomePublisherCommandHandler(IPlatformUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(BecomePublisherCommand request, CancellationToken cancellationToken)
    {
        await _userService.BecomePublisher(request.Id);
        return OperationResult.CreateSuccess();
    }
}