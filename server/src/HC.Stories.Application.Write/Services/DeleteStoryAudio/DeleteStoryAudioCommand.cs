namespace HC.Stories.Application.Write.Stories.Services.DeleteStoryAudio;

public class DeleteStoryAudioCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
}