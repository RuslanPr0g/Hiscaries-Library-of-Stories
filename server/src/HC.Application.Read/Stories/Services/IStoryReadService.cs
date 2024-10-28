using HC.Application.Read.Genres.ReadModels;
using HC.Application.Read.Stories.Queries;
using HC.Application.Read.Stories.ReadModels;

namespace HC.Application.Read.Stories.Services;

public interface IStoryReadService
{
    Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(GetStoryRecommendationsQuery request);
    Task<IEnumerable<StorySimpleReadModel>> SearchForStory(GetStoryListQuery request);
}