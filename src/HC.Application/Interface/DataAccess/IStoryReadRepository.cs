using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IStoryReadRepository
{
    Task<IReadOnlyCollection<StoryReadModel>> GetStories();
    Task<StoryReadModel> GetStory(int storyId);
    Task<List<GenreReadModel>> GetGenres();
}