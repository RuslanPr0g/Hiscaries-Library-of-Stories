﻿using HC.Application.Interface;
using HC.Domain.Stories;
using HC.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HC.Infrastructure.Repository;

public sealed class EFStoryReadRepository : IStoryReadRepository
{
    private readonly HiscaryContext _context;

    public EFStoryReadRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GenreReadModel>> GetAllGenres() =>
        await _context.Genres.AsNoTracking().Select(genre => GenreReadModel.FromDomainModel(genre)).ToListAsync();

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoriesBy(string searchTerm, string genre) =>
        await _context.Stories.AsNoTracking().Where(story =>
        story.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
        story.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
        story.Genres.Any(genre => genre.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
        story.Genres.Any(genre => genre.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
        .Select(story => StorySimpleReadModel.FromDomainModel(story, null))
        .ToListAsync();

    public async Task<StoryWithContentsReadModel?> GetStory(StoryId storyId)
    {
        return await _context.Stories.AsNoTracking()
        .Include(x => x.Publisher)
        .Include(x => x.Genres)
        .Include(x => x.Comments)
        .Include(x => x.Contents)
        .Where(story => story.Id == storyId)
        .Select(story => StoryWithContentsReadModel.FromDomainModel(story)).FirstOrDefaultAsync();
    }

    public async Task<StorySimpleReadModel?> GetStorySimpleInfo(StoryId storyId, string? requesterUsername)
    {
        return await _context.Stories.AsNoTracking()
        .Include(x => x.Publisher)
        .Where(story => story.Id == storyId)
        .Select(story => StorySimpleReadModel.FromDomainModel(story, requesterUsername)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<StorySimpleReadModel>> GetStoryRecommendations(string username)
    {
        // TODO: make it less dumb

        return await _context.Stories
            .Include(x => x.Publisher)
            .Select(story => StorySimpleReadModel.FromDomainModel(story, null))
            .ToListAsync();

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

        var historyOfTheUser = user.ReadHistory;

        var rnd = new Random(user.Username.Length);

        var randomStory = historyOfTheUser.Skip(rnd.Next(0, historyOfTheUser.Count - 1)).Take(1).Single();

        var splitTitleInWords = randomStory.Story.Title.Trim().Split(' ');

        var randomWordFromTitle = splitTitleInWords.Skip(rnd.Next(0, splitTitleInWords.Length - 1)).Take(1).Single();

        var randomSearchTerm = randomWordFromTitle.Replace(',', ' ').Replace('.', ' ').Replace(';', ' ').Trim();

        var genresCount = randomStory.Story.Genres.Count;

        var randomStoryGenre = randomStory.Story.Genres.Skip(rnd.Next(0, genresCount - 1)).Take(1).FirstOrDefault()?.Name;

        var allGenresCount = await _context.Genres.CountAsync();

        randomStoryGenre ??= (await _context.Genres.Skip(rnd.Next(0, allGenresCount - 1)).Take(1).SingleOrDefaultAsync()).Name;

        return await GetStoriesBy(randomSearchTerm ?? randomStoryGenre, randomStoryGenre);
    }
}
