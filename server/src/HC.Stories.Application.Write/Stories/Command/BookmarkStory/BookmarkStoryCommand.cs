namespace HC.Application.Write.Stories.Command;

public class BookmarkStoryCommand : IRequest<OperationResult>
{
    public Guid UserId { get; set; }
    public Guid StoryId { get; set; }
}