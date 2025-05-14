namespace HC.Stories.Application.Write.Stories.Services.ReadStory;

public class ReadStoryCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public Guid UserId { get; set; }
    public int Page { get; set; }
}