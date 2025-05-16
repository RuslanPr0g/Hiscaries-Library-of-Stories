using HC.Stories.Domain.DataAccess;
using HC.Stories.Domain.Stories;
using HC.Stories.Persistence.Context;

namespace HC.Stories.Persistence.Write;

public class StoryWriteRepository(StoriesContext context)
    : BaseWriteRepository<Story, StoryId, StoriesContext>,
    IStoryWriteRepository
{
    protected override StoriesContext Context { get; init; } = context;
}
