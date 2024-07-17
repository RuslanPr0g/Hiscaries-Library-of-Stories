using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Application.Models.Response;
using HC.Domain.Story;

namespace HC.Application.Interface;

public interface IStoryPageService
{
    Task<AddStoryPageResult> AddPageForStory(StoryPage storyPage);
    Task<IList<StoryPage>> GetStoryPages(int storyId);
    Task RemoveStoryPages(int storyId);
}