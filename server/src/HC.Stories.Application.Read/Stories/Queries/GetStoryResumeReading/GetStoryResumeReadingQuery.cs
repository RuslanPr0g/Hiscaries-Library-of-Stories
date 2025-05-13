using HC.Application.Read.Stories.ReadModels;

namespace HC.Application.Read.Stories.Queries;

public sealed class GetStoryResumeReadingQuery : IRequest<IEnumerable<StorySimpleReadModel>>
{
    public Guid UserId { get; set; }
}