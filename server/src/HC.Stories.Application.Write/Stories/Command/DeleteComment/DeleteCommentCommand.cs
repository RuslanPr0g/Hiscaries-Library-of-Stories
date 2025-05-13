namespace HC.Stories.Application.Write.Stories.Command.DeleteComment;

public class DeleteCommentCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public Guid CommentId { get; set; }
}