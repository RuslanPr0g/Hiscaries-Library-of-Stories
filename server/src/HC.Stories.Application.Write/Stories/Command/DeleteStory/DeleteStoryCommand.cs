namespace HC.Application.Write.Stories.Command;

public class DeleteStoryCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
}