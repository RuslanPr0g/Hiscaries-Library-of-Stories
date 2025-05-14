namespace HC.Stories.Application.Write.Stories.Services.AddComment;

public class AddCommentCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; init; }
    public int Score { get; init; }
}