using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Services;
using MediatR;

namespace HC.Application.Write.Stories.Command;

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