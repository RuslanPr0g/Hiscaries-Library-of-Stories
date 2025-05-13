using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Command.ScoreStory;

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