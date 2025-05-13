using HC.Stories.Application.Read.Genres.ReadModels;
using HC.Stories.Application.Read.Stories.Queries.GetStoryList;
using HC.Stories.Application.Read.Stories.Queries.GetStoryReadingHistory;
using HC.Stories.Application.Read.Stories.Queries.GetStoryRecommendations;
using HC.Stories.Application.Read.Stories.Queries.GetStoryResumeReading;
using HC.Stories.Application.Read.Stories.ReadModels;

namespace HC.Stories.Application.Read.Stories.Services;

public interface IStoryReadService
{
    Task<StoryWithContentsReadModel?> GetStoryById(Guid storyId, Guid searchedBy);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(GetStoryRecommendationsQuery request);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(GetStoryResumeReadingQuery request);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(GetStoryReadingHistoryQuery request);
    Task<IEnumerable<StorySimpleReadModel>> SearchForStory(GetStoryListQuery request);
}