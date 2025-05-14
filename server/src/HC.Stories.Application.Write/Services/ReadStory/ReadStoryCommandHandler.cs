namespace HC.Stories.Application.Write.Stories.Services.ReadStory;

internal class ReadStoryCommandHandler : IRequestHandler<ReadStoryCommand, OperationResult>
{
    private readonly IPlatformUserWriteService _service;

    public ReadStoryCommandHandler(IPlatformUserWriteService storyService)
    {
        _service = storyService;
    }

    public async Task<OperationResult> Handle(ReadStoryCommand request, CancellationToken cancellationToken)
    {
        return await _service.ReadStoryHistory(request);
    }
}