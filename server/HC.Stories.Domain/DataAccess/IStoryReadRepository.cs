using Enterprise.Domain.DataAccess;
using HC.Stories.Domain.ReadModels;
using HC.Stories.Domain.Stories;

namespace HC.Stories.Domain.DataAccess;

public interface IStoryReadRepository : IBaseReadRepository<StorySimpleReadModel>
{
    Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions();
    Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading();
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory();
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<StoryWithContentsReadModel?> GetStory(StoryId storyId);
    Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId);
    Task<IEnumerable<StorySimpleReadModel>?> GetStorySimpleInfoByLibraryId(Guid libraryId);
    Task<IEnumerable<StorySimpleReadModel>> GetLastNStories(int n);
}