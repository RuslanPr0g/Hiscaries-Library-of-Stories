using HC.Stories.Application.Read.Stories.ReadModels;
using HC.Stories.Application.Read.Stories.Services;

namespace HC.Stories.Application.Read.Stories.Queries.GetStoryList;

public class GetStoryListQueryHandler : IRequestHandler<GetStoryListQuery, IEnumerable<StorySimpleReadModel>>
{
    private readonly IStoryReadService _storySevice;

    public GetStoryListQueryHandler(IStoryReadService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> Handle(GetStoryListQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.SearchForStory(request);
    }
}