using HC.Stories.Application.Read.Stories.ReadModels;
using HC.Stories.Application.Read.Stories.Services;

namespace HC.Stories.Application.Read.Services.GetStoryWithContents;

public class GetStoryWithContentsHandler : IRequestHandler<GetStoryWithContentsQuery, StoryWithContentsReadModel?>
{
    private readonly IStoryReadService _storySevice;

    public GetStoryWithContentsHandler(IStoryReadService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<StoryWithContentsReadModel?> Handle(GetStoryWithContentsQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetStoryById(request.Id, request.UserId);
    }
}