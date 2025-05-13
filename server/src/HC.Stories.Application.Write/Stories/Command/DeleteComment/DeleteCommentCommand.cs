namespace HC.Application.Write.Stories.Command;

public class DeleteCommentCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public Guid CommentId { get; set; }
}