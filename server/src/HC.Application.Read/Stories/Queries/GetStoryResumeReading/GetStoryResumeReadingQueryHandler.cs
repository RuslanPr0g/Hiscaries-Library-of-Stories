using HC.Application.Read.Stories.ReadModels;
using HC.Application.Read.Stories.Services;
using MediatR;

namespace HC.Application.Read.Stories.Queries;

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