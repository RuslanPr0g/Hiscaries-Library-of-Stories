using HC.Application.Constants;
using HC.Application.Write.PlatformUsers.Services;
using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.PlatformUsers.Command.PublishReview;

public class PublishReviewCommandHandler : IRequestHandler<PublishReviewCommand, OperationResult>
{
    private readonly IUserWriteService _userService;

    public PublishReviewCommandHandler(IUserWriteService userService)
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