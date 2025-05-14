using HC.Stories.Domain.ReadModels;

namespace HC.Stories.Domain.Services;

public interface IStoryReadService
{
    Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId, Guid searchedBy);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(GetStoryRecommendationsQuery request);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(GetStoryResumeReadingQuery request);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(GetStoryReadingHistoryQuery request);
    Task<IEnumerable<StorySimpleReadModel>> SearchForStory(GetStoryListQuery request);
}