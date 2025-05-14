namespace HC.Stories.Application.Write.Stories.Services.BookmarkStory;

public class BookmarkStoryCommand : IRequest<OperationResult>
{
    public Guid UserId { get; set; }
    public Guid StoryId { get; set; }
}