namespace HC.Stories.Application.Write.Stories.Services.ScoreStory;

public class StoryScoreCommand : IRequest<OperationResult>
{
    public Guid StoryId { get; set; }
    public Guid UserId { get; set; }
    public int Score { get; set; }
}