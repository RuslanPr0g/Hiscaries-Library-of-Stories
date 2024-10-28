using HC.Application.Write.Stories.DataAccess;
using HC.Domain.Stories;
using HC.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace HC.Persistence.Write.Repositories;

public sealed class EFStoryWriteRepository : IStoryWriteRepository
{
    private readonly HiscaryContext _context;

    public EFStoryWriteRepository(HiscaryContext context)
    {
        _context = context;
    }

    public async Task<Genre?> GetGenre(GenreId genreId) =>
        await _context.Genres.FirstOrDefaultAsync(x => x.Id == genreId);

    public async Task AddGenre(Genre genre) => await _context.Genres.AddAsync(genre);

    public async Task AddStory(Story story) =>
        await _context.Stories.AddAsync(story);

    public void DeleteGenre(Genre genre) => _context.Genres.Remove(genre);

    public void DeleteStory(Story story) =>
        _context.Stories.Remove(story);

    public async Task<Story?> GetStory(StoryId storyId) =>
        await _context.Stories.FirstOrDefaultAsync(x => x.Id == storyId);

    public async Task<ICollection<Genre>> GetGenresByIds(Guid[] genreIds) =>
        await _context.Genres.Where(g => genreIds.Contains(g.Id)).ToListAsync();
}