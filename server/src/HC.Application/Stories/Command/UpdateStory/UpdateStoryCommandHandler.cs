using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

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