using HC.Application.Common.Extensions;
using HC.Application.Read.Genres.ReadModels;
using HC.Application.Read.Stories.DataAccess;
using HC.Application.Read.Stories.ReadModels;
using HC.Domain.Stories;
using HC.Domain.Users;
using HC.Persistence.Context;
using MassTransit.Initializers;
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

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre, UserId searchedBy)
    {
        // TODO: improve the search depth, etc.
        var stories = (await _context.Stories.AsNoTracking()
            .Where(story =>
                EF.Functions.ILike(story.Title, $"%{searchTerm}%") ||
                EF.Functions.ILike(story.Description, $"%{searchTerm}%"))
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.UserId == searchedBy)
                    .Select(history => (int?)history.LastPageRead)
                    .FirstOrDefault()
            })
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(storyInformation.Story, storyInformation.LastPageRead, null));

        return stories;
    }

    public async Task<StoryWithContentsReadModel?> GetStory(StoryId storyId, UserId searchedBy)
    {
        var storyInformation = await _context.Stories.AsNoTracking()
            .Include(x => x.Publisher)
            .Include(x => x.Genres)
            .Include(x => x.Comments)
            .Include(x => x.Contents)
            .Where(story => story.Id == storyId)
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.UserId == searchedBy)
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

    public async Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, UserId searchedBy, string? requesterUsername)
    {
        var storyInformation = await _context.Stories.AsNoTracking()
            .Include(x => x.Publisher)
            .Where(story => story.Id == storyId)
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.UserId == searchedBy)
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

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryReadingSuggestions(UserId searchedBy)
    {
        // TODO: make it less dumb

        var stories = (await _context.Stories
            .Include(x => x.Publisher)
            .Include(x => x.Contents)
            .Where(x => x.Contents.Any())
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.UserId == searchedBy)
                    .Select(history => (int?)history.LastPageRead)
                    .FirstOrDefault()
            })
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(storyInformation.Story, storyInformation.LastPageRead, null));

        return stories;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryResumeReading(UserId searchedBy)
    {
        var stories = (await _context.Stories
            .Include(x => x.Publisher)
            .Include(x => x.Contents)
            .Where(x => x.ReadHistory.Any(x => x.UserId == searchedBy))
            .Select(story => new
            {
                Story = story,
                LastPageRead = story.ReadHistory
                    .Where(history => history.UserId == searchedBy)
                    .Select(history => (int?)history.LastPageRead)
                    .FirstOrDefault()
            })
            .Take(3)
            .OrderByDescending(x => x.LastPageRead)
            .ToListAsync())
            .Select(storyInformation => StoryDomainToSimpleReadDto(storyInformation.Story, storyInformation.LastPageRead, null));

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
