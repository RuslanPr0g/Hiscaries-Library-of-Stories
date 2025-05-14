using HC.Stories.Application.Read.Stories.ReadModels;

namespace HC.Stories.Application.Read.Services.GetStoryResumeReading;

public sealed class GetStoryResumeReadingQuery : IRequest<IEnumerable<StorySimpleReadModel>>
{
    public Guid UserId { get; set; }
}