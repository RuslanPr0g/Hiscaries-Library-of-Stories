using HC.Application.Genres.ReadModels;
using HC.Application.Stories.ReadModels;
using HC.Domain.Stories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.Stories.DataAccess;

public interface IStoryReadRepository
{
    Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(string username);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<StoryWithContentsReadModel?> GetStory(StoryId storyId);
    Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, string? requesterUsername);
}