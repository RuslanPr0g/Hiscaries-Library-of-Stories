using HC.Domain.Stories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IStoryReadRepository
{
    Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre);
    Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(string username);
    Task<IEnumerable<GenreReadModel>> GetAllGenres();
    Task<StoryReadModel?> GetStory(StoryId storyId);
    Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, string? requesterUsername);
}