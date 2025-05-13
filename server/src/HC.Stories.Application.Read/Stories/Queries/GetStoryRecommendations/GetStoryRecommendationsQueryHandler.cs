using HC.Stories.Application.Read.Stories.ReadModels;
using HC.Stories.Application.Read.Stories.Services;

namespace HC.Stories.Application.Read.Stories.Queries.GetStoryRecommendations;

public class GetStoryRecommendationsQueryHandler : IRequestHandler<GetStoryRecommendationsQuery, IEnumerable<StorySimpleReadModel>>
{
    private readonly IStoryReadService _storySevice;

    public GetStoryRecommendationsQueryHandler(IStoryReadService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> Handle(GetStoryRecommendationsQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetStoryRecommendations(request);
    }
}