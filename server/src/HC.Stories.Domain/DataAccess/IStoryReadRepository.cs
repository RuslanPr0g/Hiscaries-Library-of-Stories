using Enterprise.Domain.DataAccess;
using HC.Stories.Domain.ReadModels;
using HC.Stories.Domain.Stories;

namespace HC.Stories.Domain.DataAccess;

public interface IStoryReadRepository : IBaseReadRepository<StorySimpleReadModel>
{
    Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre, Guid searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions(Guid searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(Guid searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(Guid searchedBy);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<StoryWithContentsReadModel?> GetStory(StoryId storyId, Guid searchedBy);
    Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, Guid searchedBy, string? requesterUsername);
    Task<IEnumerable<StorySimpleReadModel>?> GetStorySimpleInfoByLibraryId(Guid libraryId, Guid searchedBy, string? requesterUsername);
    Task<IEnumerable<StorySimpleReadModel>> GetLastNStories(int n);
}