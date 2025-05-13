using HC.Application.Read.Stories.ReadModels;
using HC.Application.Read.Stories.Services;

namespace HC.Application.Read.Stories.Queries;

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