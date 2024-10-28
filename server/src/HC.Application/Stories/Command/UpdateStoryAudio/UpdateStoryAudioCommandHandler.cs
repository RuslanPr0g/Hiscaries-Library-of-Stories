using HC.Application.ResultModels.Response;
using HC.Application.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Stories.Command;

public class UpdateStoryAudioCommandHandler : IRequestHandler<UpdateStoryAudioCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public UpdateStoryAudioCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(UpdateStoryAudioCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.UpdateAudio(request);
    }
}