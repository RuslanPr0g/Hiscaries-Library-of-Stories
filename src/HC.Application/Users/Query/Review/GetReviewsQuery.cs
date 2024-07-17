using System.Collections.Generic;
using HC.Domain.User;
using MediatR;

namespace HC.Application.Users.Query;

public class GetReviewsQuery : IRequest<List<Review>>
{
}