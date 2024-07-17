using HC.Infrastructure.DataAccess;
using HC.Application.Interface;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HC.Domain.Story;
using HC.Domain.Story.Comment;

namespace HC.Infrastructure.Repository
{
    public class EFStoryRepository : IStoryWriteRepository
    {
        private readonly HiscaryContext _context;

        public EFStoryRepository(HiscaryContext context)
        {
            _context = context;
        }

        public Task<IReadOnlyCollection<Story>> GetStories()
        {
            throw new System.NotImplementedException();
        }

        public Task<Story> GetStory(int storyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetNumberOfPages(int storyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StoryPage>> GetStoryPages(int storyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetStoryPagesCount(int storyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteStory(int storyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddStory(Story story)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StoryRating>> GetStoryScores()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Comment>> GetStoryComments()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StoryReadHistory>> GetStoryReadHistory()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StoryReadHistoryProgress>> GetStoryReadHistoryProgress(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddPage(StoryPage page)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateStory(Story story)
        {
            throw new System.NotImplementedException();
        }

        public Task RemovePagesFromStory(int storyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ReadStoryHistory(StoryReadHistory story)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> TotalReadsForUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> TotalStoriesForUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<double> AverageScoreForUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Genre>> GetGenres()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StoryBookMark>> GetBookMarks()
        {
            throw new System.NotImplementedException();
        }

        public Task UnBookMarkStory(StoryBookMark bookMark)
        {
            throw new System.NotImplementedException();
        }

        public Task BookMarkStory(StoryBookMark bookMark)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Story>> GetStoryBookMarks(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddStoryComment(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> AddGenre(Genre genre)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateGenre(Genre genre)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteGenre(Genre genre)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateStoryComment(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteStoryComment(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateStoryScore(StoryRating storyScore)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertStoryScore(StoryRating storyScore)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StoryAudio>> GetAudio(int storyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAudio(int[] audioIds)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CreateAudio(StoryAudio storyAudio)
        {
            throw new System.NotImplementedException();
        }

        public Task<StoryAudio> GetAudioById(int audioId)
        {
            throw new System.NotImplementedException();
        }
    }
}

