using HC.Stories.Application.Read.Stories.ReadModels;
using HC.Stories.Application.Read.Stories.Services;

namespace HC.Stories.Application.Read.Services.GetStoryReadingHistory;

public class GetStoryReadingHistoryQueryHandler : IRequestHandler<GetStoryReadingHistoryQuery, IEnumerable<StorySimpleReadModel>>
{
    private readonly IStoryReadService _storySevice;

    public GetStoryReadingHistoryQueryHandler(IStoryReadService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> Handle(GetStoryReadingHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetStoryReadingHistory(request);
    }
}