namespace HC.Application.Write.Stories.Command;

public class UpdateStoryAudioCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public string Name { get; set; }
    public byte[] Audio { get; set; }
}