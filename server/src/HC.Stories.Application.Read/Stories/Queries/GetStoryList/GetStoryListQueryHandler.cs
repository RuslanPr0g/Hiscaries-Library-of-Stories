using HC.Application.Read.Stories.ReadModels;
using HC.Application.Read.Stories.Services;
using MediatR;

namespace HC.Application.Read.Stories.Queries;

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