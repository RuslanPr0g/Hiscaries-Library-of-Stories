namespace HC.Stories.Application.Write.Stories.Services.DeleteStory;

public class DeleteStoryCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
}