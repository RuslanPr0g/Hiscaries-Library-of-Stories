using HC.Stories.Application.Write.Stories.Services;

namespace HC.Stories.Application.Write.Stories.Services.DeleteStoryAudio;

public class DeleteStoryAudioCommandHandler : IRequestHandler<DeleteStoryAudioCommand, OperationResult>
{
    private readonly IStoryWriteService _storyService;

    public DeleteStoryAudioCommandHandler(IStoryWriteService storyService)
    {
        _storyService = storyService;
    }

    public async Task<OperationResult> Handle(DeleteStoryAudioCommand request, CancellationToken cancellationToken)
    {
        return await _storyService.DeleteAudio(request);
    }
}