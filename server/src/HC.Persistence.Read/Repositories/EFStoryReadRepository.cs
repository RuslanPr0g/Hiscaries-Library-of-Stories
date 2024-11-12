using HC.Application.Common.Extensions;
using HC.Application.Read.Genres.ReadModels;
using HC.Application.Read.Stories.DataAccess;
using HC.Application.Read.Stories.ReadModels;
using HC.Domain.Stories;
using HC.Domain.Users;
using HC.Persistence.Context;
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
        var stories = await _context.Stories.AsNoTracking()
            .Where(story =>
                EF.Functions.ILike(story.Title, $"%{searchTerm}%") ||
                EF.Functions.ILike(story.Description, $"%{searchTerm}%"))
            .Select(story => StoryDomainToSimpleReadDto(story, searchedBy, null))
            .ToListAsync();

        return await Task.WhenAll(stories);
    }

    public async Task<StoryWithContentsReadModel?> GetStory(StoryId storyId, UserId searchedBy)
    {
        var story = await _context.Stories.AsNoTracking()
            .Include(x => x.Publisher)
            .Include(x => x.Genres)
            .Include(x => x.Comments)
            .Include(x => x.Contents)
            .Where(story => story.Id == storyId)
            .Select(story => StoryDomainToReadDto(story, searchedBy))
            .FirstOrDefaultAsync();

        if (story is null)
        {
            return null;
        }

        return await story;
    }

    public async Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, UserId searchedBy, string? requesterUsername)
    {
        var story = await _context.Stories.AsNoTracking()
            .Include(x => x.Publisher)
            .Where(story => story.Id == storyId)
            .Select(story => StoryDomainToSimpleReadDto(story, searchedBy, requesterUsername))
            .FirstOrDefaultAsync();

        if (story is null)
        {
            return null;
        }

        return await story;
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(UserId searchedBy)
    {
        // TODO: make it less dumb

        var stories = await _context.Stories
            .Include(x => x.Publisher)
            .Include(x => x.Contents)
            .Where(x => x.Contents.Any())
            .Select(story => StoryDomainToSimpleReadDto(story, searchedBy, null))
            .ToListAsync();

        return await Task.WhenAll(stories);

        //var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

        //var historyOfTheUser = user.ReadHistory;

        //var rnd = new Random(user.Username.Length);

        //var randomStory = historyOfTheUser.Skip(rnd.Next(0, historyOfTheUser.Count - 1)).Take(1).Single();

        //var splitTitleInWords = randomStory.Story.Title.Trim().Split(' ');

        //var randomWordFromTitle = splitTitleInWords.Skip(rnd.Next(0, splitTitleInWords.Length - 1)).Take(1).Single();

        //var randomSearchTerm = randomWordFromTitle.Replace(',', ' ').Replace('.', ' ').Replace(';', ' ').Trim();

        //var genresCount = randomStory.Story.Genres.Count;

        //var randomStoryGenre = randomStory.Story.Genres.Skip(rnd.Next(0, genresCount - 1)).Take(1).FirstOrDefault()?.Name;

        //var allGenresCount = await _context.Genres.CountAsync();

        //randomStoryGenre ??= (await _context.Genres.Skip(rnd.Next(0, allGenresCount - 1)).Take(1).SingleOrDefaultAsync()).Name;

        //return await GetStoriesBy(randomSearchTerm ?? randomStoryGenre, randomStoryGenre);
    }

    private async Task<decimal> CalculateStoryReadingProgressForAUser(UserId userId, StoryId storyId, int totalPages)
    {
        var readingHistory = await _context.ReadHistory
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.StoryId == storyId && x.UserId == userId);

        if (readingHistory is null)
        {
            return 0;
        }

        int currentPage = readingHistory.LastPageRead;

        return currentPage.PercentageOf(totalPages);
    }

    private async Task<StorySimpleReadModel> StoryDomainToSimpleReadDto(Story? story, UserId userId, string? requesterUsername = null)
    {
        if (story is null)
        {
            return null;
        }

        var percentageRead = await CalculateStoryReadingProgressForAUser(userId, story.Id, story.TotalPages);
        return StorySimpleReadModel.FromDomainModel(story, percentageRead, requesterUsername);
    }

    private async Task<StoryWithContentsReadModel?> StoryDomainToReadDto(Story? story, UserId userId)
    {
        if (story is null)
        {
            return null;
        }

        var percentageRead = await CalculateStoryReadingProgressForAUser(userId, story.Id, story.TotalPages);
        return StoryWithContentsReadModel.FromDomainModel(story, percentageRead);
    }
}
