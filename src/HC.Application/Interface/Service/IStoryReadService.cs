using HC.Domain.Stories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IStoryReadService
{
    Task<StoryReadModel> GetStoryById(StoryId storyId);
    Task<IEnumerable<StoryReadModel>> GetAllStories();
}