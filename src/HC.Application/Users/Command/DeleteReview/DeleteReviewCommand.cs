using HC.Application.Models.Connection;
using MediatR;

namespace HC.Application.Users.Command;

public class DeleteReviewCommand : IRequest<int>
{
    public UserConnection User { get; set; }
    public int Id { get; set; }
}