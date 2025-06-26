using Hiscary.Stories.Domain.DataAccess;
using Hiscary.Stories.Domain.Stories;
using Hiscary.Stories.Persistence.Context;

namespace Hiscary.Stories.Persistence.Write;

public class StoryWriteRepository(StoriesContext context)
    : BaseWriteRepository<Story, StoryId, StoriesContext>,
    IStoryWriteRepository
{
    protected override StoriesContext Context { get; init; } = context;
}
