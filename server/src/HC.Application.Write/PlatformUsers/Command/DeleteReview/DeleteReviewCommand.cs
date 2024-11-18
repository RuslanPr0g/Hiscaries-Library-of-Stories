using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.PlatformUsers.Command.DeleteReview;

public sealed class DeleteReviewCommand : IRequest<OperationResult>
{
    public string Username { get; set; }
    public Guid ReviewId { get; set; }
}