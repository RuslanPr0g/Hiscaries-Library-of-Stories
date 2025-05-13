using HC.Stories.Application.Read.Genres.ReadModels;
using HC.Stories.Application.Read.Stories.Services;

namespace HC.Stories.Application.Read.Stories.Queries.GetGenreList;

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