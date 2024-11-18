using HC.Application.Read.Genres.ReadModels;
using HC.Application.Read.Stories.ReadModels;
using HC.Domain.Stories;
using HC.Domain.Users;

namespace HC.Application.Read.Stories.DataAccess;

public interface IStoryReadRepository
{
    Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre, ReaderId searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions(ReaderId searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(ReaderId searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(ReaderId searchedBy);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<StoryWithContentsReadModel?> GetStory(StoryId storyId, ReaderId searchedBy);
    Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, ReaderId searchedBy, string? requesterUsername);
}