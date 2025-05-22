using HC.Stories.Domain.DataAccess;
using HC.Stories.Domain.ReadModels;
using HC.Stories.Domain.Stories;
using HC.Stories.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Stories.Persistence.Read;

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
                story.Genres.Any(g => g.Name.Contains(genre)) ||
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

    public async Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId)
    {
        var story = await Context.Stories
            .AsNoTracking()
            .Where(story => story.Id == storyId)
            .FirstOrDefaultAsync();

        if (story is null)
        {
            return null;
        }

        return StoryDomainToSimpleReadDto(story);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions()
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.Contents.Any())
            .ToListAsync())
            .Select(StoryDomainToSimpleReadDto);

        return stories!;
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

    public async Task<IEnumerable<StorySimpleReadModel>> GetLastNStories(int n)
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .Where(x => x.Contents.Any())
            .OrderByDescending(x => x.CreatedAt)
            .Take(n)
            .ToListAsync())
            .Select(StoryDomainToSimpleReadDto);

        return stories!;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading()
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Take(3)
            .ToListAsync())
            .Select(StoryDomainToSimpleReadDto);

        return stories!;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory()
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .AsSplitQuery()
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
