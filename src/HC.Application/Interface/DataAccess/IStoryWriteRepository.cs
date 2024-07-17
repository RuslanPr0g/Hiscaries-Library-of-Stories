using HC.Domain.Stories;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IStoryWriteRepository
{
    Task<Story> GetStory(StoryId storyId);
    Task<int> AddStory(Story story);
    Task<int> DeleteStory(StoryId storyId);
}