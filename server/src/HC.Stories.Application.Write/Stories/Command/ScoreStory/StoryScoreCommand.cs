namespace HC.Application.Write.Stories.Command;

public class StoryScoreCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public Guid UserId { get; set; }
    public int Score { get; set; }
}