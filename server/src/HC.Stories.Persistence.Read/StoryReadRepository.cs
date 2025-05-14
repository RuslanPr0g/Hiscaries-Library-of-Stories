using Enterprise.Domain.Extensions;
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
        await Context.Genres.AsNoTracking().Select(genre => GenreReadModel.FromDomainModel(genre)).ToListAsync();

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre, Guid searchedBy)
    {
        // TODO: improve the search depth, etc.
        var stories = (await Context.Stories
            .AsNoTracking()
            .Where(story =>
                EF.Functions.ILike(story.Title, $"%{searchTerm}%") ||
                EF.Functions.ILike(story.Description, $"%{searchTerm}%"))
            .Select(story => new
            {
                Story = story,
                // TODO: fix
                //LastPageRead = story.ReadHistory
                //    .Where(history => history.PlatformUserId == searchedBy)
                //    .Select(history => (int?)history.LastPageRead)
                //    .FirstOrDefault()
            })
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(
                storyInformation.Story, 0,
                // TODO: fix this
                //storyInformation.LastPageRead,
                null));

        return stories;
    }

    public async Task<StoryWithContentsReadModel?> GetStory(StoryId storyId, Guid searchedBy)
    {
        var storyInformation = await Context.Stories
            .AsNoTracking()
            .Include(x => x.Genres)
            .Include(x => x.Comments)
            .Include(x => x.Contents)
            .Where(story => story.Id == storyId)
            .Select(story => new
            {
                Story = story,
                // TODO: fix this
                //LastPageRead = story.ReadHistory
                //    .Where(history => history.PlatformUserId == searchedBy)
                //    .Select(history => (int?)history.LastPageRead)
                //    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (storyInformation is null)
        {
            return null;
        }

        return StoryDomainToReadDto(
            storyInformation.Story, 0
            // TODO: fix this
            //storyInformation.LastPageRead
            );
    }

    public async Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, Guid searchedBy, string? requesterUsername)
    {
        var storyInformation = await Context.Stories
            .AsNoTracking()
            .Where(story => story.Id == storyId)
            .Select(story => new
            {
                Story = story,
                // TODO: fix this
                //LastPageRead = story.ReadHistory
                //    .Where(history => history.PlatformUserId == searchedBy)
                //    .Select(history => (int?)history.LastPageRead)
                //    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (storyInformation is null)
        {
            return null;
        }

        return StoryDomainToSimpleReadDto(
            storyInformation.Story, 0,
            // TODO: fix this
            //storyInformation.LastPageRead,
            requesterUsername);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions(Guid searchedBy)
    {
        // TODO: make it less dumb

        var stories = (await Context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.Contents.Any())
            .Select(story => new
            {
                Story = story,
                // TODO: fix this
                //LastPageRead = story.ReadHistory
                //    .Where(history => history.PlatformUserId == searchedBy)
                //    .Select(history => (int?)history.LastPageRead)
                //    .FirstOrDefault()
            })
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(
                storyInformation.Story, 0,
                // TODO: fix this
                //storyInformation.LastPageRead,
                null));

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>?> GetStorySimpleInfoByLibraryId(
        Guid libraryId,
        Guid searchedBy,
        string? requesterUsername)
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.LibraryId == libraryId)
            .Select(story => new
            {
                Story = story,
                //LastPageRead = story.ReadHistory
                //    .Where(history => history.PlatformUserId == searchedBy)
                //    .Select(history => (int?)history.LastPageRead)
                //    .FirstOrDefault()
            })
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(
                storyInformation.Story, 0,
                // TODO: fix this
                //storyInformation.LastPageRead,
                null));

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetLastNStories(int n)
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .Where(x => x.Contents.Any())
            .OrderByDescending(x => x.CreatedAt)
            .Take(n)
            .ToListAsync())
            .Select(story => StoryDomainToSimpleReadDto(story, null, null));

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(Guid searchedBy)
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Select(story => new
            {
                Story = story,
                // TODO: fix this
                //LastPageRead = story.ReadHistory
                //    .Where(history => history.PlatformUserId == searchedBy)
                //    .Select(history => (int?)history.LastPageRead)
                //    .FirstOrDefault()
            })
            .Take(3)
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(
                storyInformation.Story, 0,
                // TODO: fix this
                //storyInformation.LastPageRead,
                null));

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(Guid searchedBy)
    {
        var stories = (await Context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Select(story => new
            {
                Story = story,
                // TODO: fix this
                //LastPageRead = story.ReadHistory
                //    .Where(history => history.PlatformUserId == searchedBy)
                //    .Select(history => new
                //    {
                //        PageNumber = (int?)history.LastPageRead,
                //        ReadAt = (DateTime?)history.EditedAt
                //    })
                //    .FirstOrDefault(),
            })
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(
                storyInformation.Story, 0,
                // TODO: fix this
                //storyInformation.LastPageRead?.PageNumber ?? 0,
                null));

        return stories;
    }

    private static decimal RetrieveReadingProgressForAUser(int lastPageRead, int totalPages)
    {
        int currentPage = lastPageRead + 1;
        return currentPage.PercentageOf(totalPages);
    }

    private static StorySimpleReadModel StoryDomainToSimpleReadDto(
        Story? story,
        int? lastPageReadByUser,
        string? requesterUsername = null)
    {
        if (story is null)
        {
            return null;
        }

        var lastPageRead = lastPageReadByUser is null ? 0 : lastPageReadByUser.Value;
        var percentageRead = lastPageReadByUser is null ? 0 :
            RetrieveReadingProgressForAUser(lastPageRead, story.TotalPages);
        return StorySimpleReadModel.FromDomainModel(story, percentageRead, lastPageRead, requesterUsername);
    }

    private static StoryWithContentsReadModel? StoryDomainToReadDto(
        Story? story,
        int? lastPageReadByUser)
    {
        if (story is null)
        {
            return null;
        }

        var lastPageRead = lastPageReadByUser is null ? 0 : lastPageReadByUser.Value;
        var percentageRead = lastPageReadByUser is null ? 0 :
            RetrieveReadingProgressForAUser(lastPageRead, story.TotalPages);
        return StoryWithContentsReadModel.FromDomainModel(story, percentageRead, lastPageRead);
    }
}
