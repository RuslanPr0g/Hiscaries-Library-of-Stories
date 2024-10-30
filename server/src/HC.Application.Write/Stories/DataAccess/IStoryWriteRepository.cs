using HC.Domain.Stories;

namespace HC.Application.Write.Stories.DataAccess;

public interface IStoryWriteRepository
{
    Task<Story?> GetStory(StoryId storyId);
    Task AddStory(Story story);
    void DeleteStory(Story story);

    Task<Genre?> GetGenre(GenreId genreId);
    Task AddGenre(Genre genre);
    void DeleteGenre(Genre genre);
    Task<ICollection<Genre>> GetGenresByIds(Guid[] genreIds);
}