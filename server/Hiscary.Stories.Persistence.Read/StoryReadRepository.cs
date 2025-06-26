using Hiscary.Persistence.Extensions;
using Hiscary.Stories.Domain.DataAccess;
using Hiscary.Stories.Domain.ReadModels;
using Hiscary.Stories.Domain.Stories;
using Hiscary.Stories.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Hiscary.Stories.Persistence.Read;

public class StoryReadRepository(StoriesContext context) :
    BaseReadRepository<StorySimpleReadModel>,
    IStoryReadRepository
{
    private StoriesContext Context { get; init; } = context;

    public async Task<IEnumerable<GenreReadModel>> GetAllGenres() =>
        await Context.Genres.AsNoTracking()
            .Select(genre => GenreReadModel.FromDomainModel(genre))
            .ToListAsync();

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre)
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .Include(_ => _.Genres)
            .Where(story =>
                (!string.IsNullOrWhiteSpace(genre) && story.Genres.Any(g => g.Name.Contains(genre))) ||
                EF.Functions.ILike(story.Title, $"%{searchTerm}%") ||
                EF.Functions.ILike(story.Description, $"%{searchTerm}%"))
            .ToListAsync())
            .Select(StoryDomainToSimpleReadDto);

        return stories!;
    }

    public async Task<StoryWithContentsReadModel?> GetStory(StoryId storyId)
    {
        var story = await Context.Stories
            .AsNoTracking()
            .Include(x => x.Genres)
            .Include(x => x.Comments)
            .Include(x => x.Contents)
            .Where(story => story.Id == storyId)
            .FirstOrDefaultAsync();

        if (story is null)
        {
            return null;
        }

        return StoryDomainToReadDto(story);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStorySimpleInfo(
        StoryId[] storyIds,
        string sortProperty = "CreatedAt",
        bool sortAsc = true)
    {
        var query = Context.Stories
            .AsNoTracking()
            .Where(story => storyIds.Contains(story.Id))
            .OrderByProperty(sortProperty, sortAsc);

        var stories = await query.ToListAsync();

        return stories.SelectSkipNulls(StoryDomainToSimpleReadDto);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions()
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.Contents.Any())
            .ToListAsync())
            .SelectSkipNulls(StoryDomainToSimpleReadDto);

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>?> GetStorySimpleInfoByLibraryId(Guid libraryId)
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.LibraryId == libraryId)
            .ToListAsync())
            .Select(StoryDomainToSimpleReadDto);

        return stories!;
    }

    private static StorySimpleReadModel? StoryDomainToSimpleReadDto(Story? story)
    {
        if (story is null)
        {
            return null;
        }

        return StorySimpleReadModel.FromDomainModel(story);
    }

    private static StoryWithContentsReadModel? StoryDomainToReadDto(Story? story)
    {
        if (story is null)
        {
            return null;
        }

        return StoryWithContentsReadModel.FromDomainModel(story);
    }
}
