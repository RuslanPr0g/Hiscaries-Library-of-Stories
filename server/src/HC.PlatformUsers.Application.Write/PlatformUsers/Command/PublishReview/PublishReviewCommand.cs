using HC.Application.Write.ResultModels.Response;
using MediatR;

namespace HC.Application.Write.PlatformUsers.Command.PublishReview;

public sealed class PublishReviewCommand : IRequest<OperationResult>
{
    public Guid LibraryId { get; set; }
    public Guid ReviewerId { get; set; }
    public string? Message { get; set; }
    public Guid? ReviewId { get; set; }
}