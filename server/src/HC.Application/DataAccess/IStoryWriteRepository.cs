using HC.Domain.Stories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HC.Application.DataAccess;

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