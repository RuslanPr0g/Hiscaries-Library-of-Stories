using Microsoft.EntityFrameworkCore;

namespace HC.Persistence.Read.Repositories;

public sealed class EFStoryReadRepository : IStoryReadRepository
{
    private readonly HiscaryContext _context;

    public EFStoryReadRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GenreReadModel>> GetAllGenres() =>
        await _context.Genres.AsNoTracking().Select(genre => GenreReadModel.FromDomainModel(genre)).ToListAsync();

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre, PlatformUserId searchedBy)
    {
        // TODO: improve the search depth, etc.
        var stories = (await _context.Stories
            .AsNoTracking()
            .Include(x => x.Library)
                .ThenInclude(x => x.PlatformUser)
            .Where(story =>
                EF.Functions.ILike(story.Title, $"%{searchTerm}%") ||
                EF.Functions.ILike(story.Description, $"%{searchTerm}%"))
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.PlatformUserId == searchedBy)
                    .Select(history => (int?)history.LastPageRead)
                    .FirstOrDefault()
            })
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(storyInformation.Story, storyInformation.LastPageRead, null));

        return stories;
    }

    public async Task<StoryWithContentsReadModel?> GetStory(StoryId storyId, PlatformUserId searchedBy)
    {
        var storyInformation = await _context.Stories
            .AsNoTracking()
            .Include(x => x.Library)
                .ThenInclude(x => x.PlatformUser)
            .Include(x => x.Genres)
            .Include(x => x.Comments)
            .Include(x => x.Contents)
            .Where(story => story.Id == storyId)
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.PlatformUserId == searchedBy)
                    .Select(history => (int?)history.LastPageRead)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (storyInformation is null)
        {
            return null;
        }

        return StoryDomainToReadDto(storyInformation.Story, storyInformation.LastPageRead);
    }

    public async Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, PlatformUserId searchedBy, string? requesterUsername)
    {
        var storyInformation = await _context.Stories
            .AsNoTracking()
            .Include(x => x.Library)
                .ThenInclude(x => x.PlatformUser)
            .Where(story => story.Id == storyId)
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.PlatformUserId == searchedBy)
                    .Select(history => (int?)history.LastPageRead)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (storyInformation is null)
        {
            return null;
        }

        return StoryDomainToSimpleReadDto(storyInformation.Story, storyInformation.LastPageRead, requesterUsername);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions(PlatformUserId searchedBy)
    {
        // TODO: make it less dumb

        var stories = (await _context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Library)
                .ThenInclude(x => x.PlatformUser)
            .Where(x => x.Contents.Any())
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.PlatformUserId == searchedBy)
                    .Select(history => (int?)history.LastPageRead)
                    .FirstOrDefault()
            })
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(storyInformation.Story, storyInformation.LastPageRead, null));

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>?> GetStorySimpleInfoByLibraryId(LibraryId libraryId, PlatformUserId searchedBy, string? requesterUsername)
    {
        var stories = (await _context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Library)
                .ThenInclude(x => x.PlatformUser)
            .Where(x => x.LibraryId == libraryId)
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.PlatformUserId == searchedBy)
                    .Select(history => (int?)history.LastPageRead)
                    .FirstOrDefault()
            })
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(storyInformation.Story, storyInformation.LastPageRead, null));

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetLastNStories(int n)
    {
        var stories = (await _context.Stories
            .AsNoTracking()
            .Include(x => x.Library)
                .ThenInclude(x => x.PlatformUser)
            .Where(x => x.Contents.Any())
            .OrderByDescending(x => x.CreatedAt)
            .Take(n)
            .ToListAsync())
            .Select(story => StoryDomainToSimpleReadDto(story, null, null));

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(PlatformUserId searchedBy)
    {
        var stories = (await _context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Library)
                .ThenInclude(x => x.PlatformUser)
            .Include(x => x.ReadHistory)
            .Where(x => x.ReadHistory.Any(x => x.PlatformUserId == searchedBy))
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.PlatformUserId == searchedBy)
                    .Select(history => (int?)history.LastPageRead)
                    .FirstOrDefault()
            })
            .Take(3)
            .OrderByDescending(x => x.LastPageRead)
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(storyInformation.Story, storyInformation.LastPageRead, null));

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingHistory(PlatformUserId searchedBy)
    {
        var stories = (await _context.Stories
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Library)
                .ThenInclude(x => x.PlatformUser)
            .Include(x => x.ReadHistory)
            .Where(x => x.ReadHistory.Any(x => x.PlatformUserId == searchedBy))
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.PlatformUserId == searchedBy)
                    .Select(history => new
                    {
                        PageNumber = (int?)history.LastPageRead,
                        ReadAt = (DateTime?)history.EditedAt
                    })
                    .FirstOrDefault(),
            })
            .Where(x => x.LastPageRead != null)
            .OrderByDescending(x => x.LastPageRead!.ReadAt)
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(storyInformation.Story, storyInformation.LastPageRead?.PageNumber ?? 0, null));

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
