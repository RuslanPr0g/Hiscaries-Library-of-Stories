using HC.Application.Read.Genres.ReadModels;
using HC.Application.Read.Stories.Services;

namespace HC.Application.Read.Stories.Queries;

public class GetGenreListQueryHandler : IRequestHandler<GetGenreListQuery, IEnumerable<GenreReadModel>>
{
    private readonly IStoryReadService _storySevice;

    public GetGenreListQueryHandler(IStoryReadService storyService)
    {
        _storySevice = storyService;
    }

    public async Task<IEnumerable<GenreReadModel>> Handle(GetGenreListQuery request, CancellationToken cancellationToken)
    {
        return await _storySevice.GetAllGenres();
    }
}