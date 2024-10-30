using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Services;
using MediatR;

namespace HC.Application.Write.Stories.Command;

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