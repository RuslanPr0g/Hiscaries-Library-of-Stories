using HC.Application.Read.Genres.ReadModels;
using HC.Application.Read.Stories.ReadModels;
using HC.Domain.Stories;

namespace HC.Application.Read.Stories.DataAccess;

public interface IStoryReadRepository
{
    Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre, PlatformUserId searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions(PlatformUserId searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(PlatformUserId searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(PlatformUserId searchedBy);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<StoryWithContentsReadModel?> GetStory(StoryId storyId, PlatformUserId searchedBy);
    Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, PlatformUserId searchedBy, string? requesterUsername);
    Task<IEnumerable<StorySimpleReadModel>> GetLastNStories(int n);
}