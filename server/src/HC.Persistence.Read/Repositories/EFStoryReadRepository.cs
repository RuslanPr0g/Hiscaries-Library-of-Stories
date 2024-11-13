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
            .ToListAsync())
            .Select(story => StoryDomainToSimpleReadDto(_context, story, searchedBy, null));

        return await stories.WhenAllSequentialAsync();
    }

    public async Task<StoryWithContentsReadModel?> GetStory(StoryId storyId, UserId searchedBy)
    {
        var story = await _context.Stories.AsNoTracking()
            .Include(x => x.Publisher)
            .Include(x => x.Genres)
            .Include(x => x.Comments)
            .Include(x => x.Contents)
            .Where(story => story.Id == storyId)
            .FirstOrDefaultAsync();

        if (story is null)
        {
            return null;
        }

        return await StoryDomainToReadDto(_context, story, searchedBy);
    }

    public async Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, UserId searchedBy, string? requesterUsername)
    {
        var story = await _context.Stories.AsNoTracking()
            .Include(x => x.Publisher)
            .Where(story => story.Id == storyId)
            .FirstOrDefaultAsync();

        if (story is null)
        {
            return null;
        }

        return await StoryDomainToSimpleReadDto(_context, story, searchedBy, requesterUsername);
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(UserId searchedBy)
    {
        // TODO: make it less dumb

        var stories = (await _context.Stories
            .Include(x => x.Publisher)
            .Include(x => x.Contents)
            .Where(x => x.Contents.Any())
            .ToListAsync())
            .Select(story => StoryDomainToSimpleReadDto(_context, story, searchedBy, null));

        return await stories.WhenAllSequentialAsync();
    }

    private static async Task<(int CurrentPage, decimal PercentageRead)> RetrieveReadingProgressForAUser(HiscaryContext context, UserId userId, StoryId storyId, int totalPages)
    {
        var readingHistory = await context.ReadHistory
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.StoryId == storyId && x.UserId == userId);

        if (readingHistory is null)
        {
            return (0, 0);
        }

        int currentPage = readingHistory.LastPageRead + 1;

        return (readingHistory.LastPageRead, currentPage.PercentageOf(totalPages));
    }

    private static async Task<StorySimpleReadModel> StoryDomainToSimpleReadDto(
        HiscaryContext context,
        Story? story,
        UserId userId,
        string? requesterUsername = null)
    {
        if (story is null)
        {
            return null;
        }

        var readingProgress = await RetrieveReadingProgressForAUser(context, userId, story.Id, story.TotalPages);
        return StorySimpleReadModel.FromDomainModel(story, readingProgress.PercentageRead, readingProgress.CurrentPage, requesterUsername);
    }

    private static async Task<StoryWithContentsReadModel?> StoryDomainToReadDto(
        HiscaryContext context,
        Story? story,
        UserId userId)
    {
        if (story is null)
        {
            return null;
        }

        var readingProgress = await RetrieveReadingProgressForAUser(context, userId, story.Id, story.TotalPages);
        return StoryWithContentsReadModel.FromDomainModel(story, readingProgress.PercentageRead, readingProgress.CurrentPage);
    }
}
