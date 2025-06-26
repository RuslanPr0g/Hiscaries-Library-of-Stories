using Hiscary.Stories.Domain.Stories;
using StackNucleus.DDD.Domain.Repositories;

namespace Hiscary.Stories.Domain.DataAccess;

public interface IStoryWriteRepository : IBaseWriteRepository<Story, StoryId>
{
}