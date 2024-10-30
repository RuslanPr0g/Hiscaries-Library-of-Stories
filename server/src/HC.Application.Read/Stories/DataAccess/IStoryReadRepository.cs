using HC.Application.Read.Genres.ReadModels;
using HC.Application.Read.Stories.ReadModels;
using HC.Domain.Stories;

namespace HC.Application.Read.Stories.DataAccess;

public interface IStoryReadRepository
{
    Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(string username);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<StoryWithContentsReadModel?> GetStory(StoryId storyId);
    Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, string? requesterUsername);
}