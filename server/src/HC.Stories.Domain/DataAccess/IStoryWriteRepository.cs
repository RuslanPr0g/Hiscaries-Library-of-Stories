using Enterprise.Domain.DataAccess;
using HC.Stories.Domain.Stories;

namespace HC.Stories.Domain.DataAccess;

public interface IStoryWriteRepository : IBaseWriteRepository<Story, StoryId>
{
}