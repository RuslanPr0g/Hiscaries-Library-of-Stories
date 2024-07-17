using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;

using MediatR;

namespace HC.Application.Users.Command;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, int>
{
    private readonly IUserService _userService;

    public DeleteReviewCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<int> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        await _userService.DeleteReview(new Review { Id = request.Id }, request.User);
        return 1;
    }
}