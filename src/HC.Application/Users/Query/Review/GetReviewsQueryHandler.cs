using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Domain.User;
using MediatR;

namespace HC.Application.Users.Query;

public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, List<Review>>
{
    private readonly IUserWriteService _userService;

    public GetReviewsQueryHandler(IUserWriteService userService)
    {
        _userService = userService;
    }

    public async Task<List<Review>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetAllReviews();
    }
}