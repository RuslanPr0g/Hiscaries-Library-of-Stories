using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Application.Interface;
using HC.Application.Models.Response;
using HC.Domain.Story;

namespace HC.Application.Services;

public class StoryPageService : IStoryPageService
{
    private readonly IStoryRepository _storyRepository;

    public StoryPageService(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task<IList<StoryPage>> GetStoryPages(int storyId)
    {
        return await _storyRepository.GetStoryPages(storyId);
    }

    public async Task<AddStoryPageResult> AddPageForStory(StoryPage storyPage)
    {
        int storyPageId = await _storyRepository.AddPage(storyPage);
        return new AddStoryPageResult(ResultStatus.Success, string.Empty, storyPageId);
    }

    public async Task RemoveStoryPages(int storyId)
    {
        await _storyRepository.RemovePagesFromStory(storyId);
    }
}