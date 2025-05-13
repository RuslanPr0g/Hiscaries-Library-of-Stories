using HC.PlatformUsers.Application.Write.PlatformUsers.Services;

namespace HC.PlatformUsers.Application.Write.PlatformUsers.Command.DeleteReview;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, OperationResult>
{
    private readonly IPlatformUserWriteService _userService;

    public DeleteReviewCommandHandler(IPlatformUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        return await _userService.DeleteReview(request);
    }
}