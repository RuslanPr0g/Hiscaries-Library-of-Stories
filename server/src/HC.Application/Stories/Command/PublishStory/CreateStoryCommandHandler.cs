using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

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