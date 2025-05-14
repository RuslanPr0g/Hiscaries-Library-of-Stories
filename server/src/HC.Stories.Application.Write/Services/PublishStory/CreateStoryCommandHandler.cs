using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Services.PublishStory;

public class CreateStoryCommandHandler : IRequestHandler<PublishStoryCommand, OperationResult<EntityIdResponse>>
{
    private readonly IStoryWriteService _storyService;

    public CreateStoryCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult<EntityIdResponse>> Handle(PublishStoryCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.PublishStory(request);
    }
}