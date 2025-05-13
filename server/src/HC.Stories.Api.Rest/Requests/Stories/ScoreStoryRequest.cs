using System;

namespace HC.API.Requests.Stories;

public class ScoreStoryRequest
{
    public Guid StoryId { get; set; }
    public int Score { get; set; }
}