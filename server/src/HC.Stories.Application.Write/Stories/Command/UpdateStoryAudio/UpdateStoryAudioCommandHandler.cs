using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Command.UpdateStoryAudio;

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