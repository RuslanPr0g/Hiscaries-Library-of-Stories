using HC.Application.Read.Genres.ReadModels;
using HC.Application.Read.Stories.ReadModels;
using HC.Domain.Stories;
using HC.Domain.Users;

namespace HC.Application.Read.Stories.DataAccess;

public interface IStoryReadRepository
{
    Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre, UserId searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions(UserId searchedBy);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(UserId searchedBy);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<StoryWithContentsReadModel?> GetStory(StoryId storyId, UserId searchedBy);
    Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, UserId searchedBy, string? requesterUsername);
}