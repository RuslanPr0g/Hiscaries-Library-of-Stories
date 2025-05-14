using HC.Stories.Application.Read.Stories.ReadModels;
using HC.Stories.Application.Read.Stories.Services;

namespace HC.Stories.Application.Read.Services.GetStoryResumeReading;

public class GetStoryResumeReadingQueryHandler : IRequestHandler<GetStoryResumeReadingQuery, IEnumerable<StorySimpleReadModel>>
{
    private readonly IStoryReadService _storySevice;

    public GetStoryResumeReadingQueryHandler(IStoryReadService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> Handle(GetStoryResumeReadingQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetStoryResumeReading(request);
    }
}