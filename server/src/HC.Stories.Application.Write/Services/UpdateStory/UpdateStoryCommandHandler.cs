using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Services.UpdateStory;

public class UpdateStoryCommandHandler : IRequestHandler<UpdateStoryCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public UpdateStoryCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(UpdateStoryCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.UpdateStory(request);
    }
}