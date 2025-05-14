namespace HC.Stories.Application.Write.Stories.Services.UpdateComment;

public class UpdateCommentCommand : IRequest<OperationResult>
{
    public Guid CommentId { get; set; }
    public Guid StoryId { get; set; }
    public string Content { get; set; }
    public int Score { get; set; }
}