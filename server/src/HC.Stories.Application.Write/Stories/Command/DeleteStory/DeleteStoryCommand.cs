namespace HC.Stories.Application.Write.Stories.Command.DeleteStory;

public class DeleteStoryCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
}