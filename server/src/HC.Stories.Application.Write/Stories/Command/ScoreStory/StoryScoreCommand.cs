namespace HC.Stories.Application.Write.Stories.Command.ScoreStory;

public class StoryScoreCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public Guid UserId { get; set; }
    public int Score { get; set; }
}