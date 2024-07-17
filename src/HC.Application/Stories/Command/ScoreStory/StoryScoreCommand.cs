using HC.Application.Models.Connection;
using MediatR;

namespace HC.Application.Stories.Command.ScoreStory;

public class StoryScoreCommand : IRequest<int>
{
    public UserConnection User { get; set; }
    public int StoryId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
}