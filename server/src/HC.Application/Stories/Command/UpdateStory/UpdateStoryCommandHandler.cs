using HC.Application.Models.Response;
using HC.Application.Services.Stories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command.UpdateStory;

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