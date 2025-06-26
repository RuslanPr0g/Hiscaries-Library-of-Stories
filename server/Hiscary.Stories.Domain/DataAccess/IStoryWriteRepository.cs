using Hiscary.Domain.DataAccess;
using Hiscary.Stories.Domain.Stories;

namespace Hiscary.Stories.Domain.DataAccess;

public interface IStoryWriteRepository : IBaseWriteRepository<Story, StoryId>
{
}