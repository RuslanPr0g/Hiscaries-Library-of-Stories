using HC.Application.Write.ResultModels.Response;
using HC.Application.Write.Stories.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HC.Application.Write.Stories.Command;

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