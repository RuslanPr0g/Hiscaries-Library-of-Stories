using HC.Application.Read.Stories.ReadModels;
using HC.Application.Read.Stories.Services;
using MediatR;

namespace HC.Application.Read.Stories.Queries;

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