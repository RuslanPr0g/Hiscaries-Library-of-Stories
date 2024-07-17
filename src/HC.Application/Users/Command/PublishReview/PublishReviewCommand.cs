using HC.Application.Models.Connection;
using MediatR;

namespace HC.Application.Users.Command.PublishReview;

public class PublishReviewCommand : IRequest<int>
{
    public UserConnection User { get; set; }
    public int Id { get; set; }
    public int PublisherId { get; set; }
    public int ReviewerId { get; set; }
    public string Message { get; set; }
}