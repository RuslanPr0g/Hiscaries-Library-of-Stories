using HC.Domain.Stories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.Interface;

public interface IStoryWriteRepository
{
    Task<Story> GetStory(StoryId storyId);
    Task<int> AddStory(Story story);
    Task<int> DeleteStory(StoryId storyId);
    Task<List<Story>> GetStoryBookMarks(int userId);
}