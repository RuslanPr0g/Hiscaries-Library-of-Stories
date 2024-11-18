using HC.Application.Write.PlatformUsers.Services;
using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.PlatformUsers.Command.DeleteReview;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, OperationResult>
{
    private readonly IUserWriteService _userService;

    public DeleteReviewCommandHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<OperationResult> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        return await _userService.DeleteReview(request);
    }
}