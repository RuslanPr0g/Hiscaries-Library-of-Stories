using HC.Application.Write.PlatformUsers.Services;

namespace HC.Application.Write.PlatformUsers.Command.PublishReview;

public class PublishReviewCommandHandler : IRequestHandler<PublishReviewCommand, OperationResult>
{
    private readonly IPlatformUserWriteService _userService;

    public PublishReviewCommandHandler(IPlatformUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(PublishReviewCommand request, CancellationToken cancellationToken)
    {
        if (request.Message is null)
        {
            return OperationResult.CreateClientSideError(UserFriendlyMessages.ReviewMessageCannotBeEmpty);
        }

        return await _userService.PublishReview(request);
    }
}