using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;

using MediatR;

namespace HC.Application.Users.Command.PublishReview;

public class PublishReviewCommandHandler : IRequestHandler<PublishReviewCommand, int>
{
    private readonly IUserService _userService;

    public PublishReviewCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<int> Handle(PublishReviewCommand request, CancellationToken cancellationToken)
    {
        return await _userService.PublishReview(new Review
        {
            Id = request.Id,
            PublisherId = request.PublisherId,
            ReviewerId = request.ReviewerId,
            Message = request.Message
        }, request.User);
    }
}