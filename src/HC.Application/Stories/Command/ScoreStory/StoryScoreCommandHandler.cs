using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Stories.Command.ScoreStory;
using HC.Domain.Story;
using HC.Domain.User;
using MediatR;

namespace HC.Application.Stories.Command;

public class StoryScoreCommandHandler : IRequestHandler<StoryScoreCommand, int>
{
    private readonly IStoryWriteService _storyService;
    private readonly IUserWriteService _userService;

    public StoryScoreCommandHandler(IStoryWriteService storyService, IUserWriteService userService)
    {
        _storyService = storyService;
        _userService = userService;
    }

    public async Task<int> Handle(StoryScoreCommand request, CancellationToken cancellationToken)
    {
        List<StoryRating> scores = await _storyService.GetScores(request.User);

        User user = await _userService.GetUserByUsername(request.User.Username, request.User);

        StoryRating exists = scores.SingleOrDefault(s => s.UserId == user.Id && s.StoryId == request.StoryId);

        if (exists is not null)
            await _storyService.UpdateStoryScore(new StoryRating
            {
                Id = exists.Id,
                UserId = exists.UserId,
                StoryId = exists.StoryId,
                Score = request.Score
            }, request.User);
        else
            await _storyService.AddStoryScore(new StoryRating
            {
                UserId = request.UserId,
                StoryId = request.StoryId,
                Score = request.Score
            }, request.User);

        return 1;
    }
}