namespace HC.PlatformUsers.Application.Write.PlatformUsers.Command.DeleteReview;

public sealed class DeleteReviewCommand : IRequest<OperationResult>
{
    public Guid UserId { get; set; }
    public Guid LibraryId { get; set; }
}