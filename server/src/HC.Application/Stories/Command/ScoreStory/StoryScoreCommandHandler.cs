using HC.Application.Models.Response;
using HC.Application.Services.Stories;
using HC.Application.Stories.Command.ScoreStory;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

public class StoryScoreCommandHandler : IRequestHandler<StoryScoreCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public StoryScoreCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(StoryScoreCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.SetStoryScoreForAUser(request);
    }
}