namespace HC.Stories.Api.Rest.Requests.Stories;

public class ScoreStoryRequest
{
    public Guid StoryId { get; set; }
    public int Score { get; set; }
}