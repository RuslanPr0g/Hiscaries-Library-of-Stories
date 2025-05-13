namespace HC.Stories.Application.Write.Stories.Command.DeleteStoryAudio;

public class DeleteStoryAudioCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
}