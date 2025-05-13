using HC.Application.Read.Stories.ReadModels;

namespace HC.Application.Read.Stories.Queries;

public sealed class GetStoryRecommendationsQuery : IRequest<IEnumerable<StorySimpleReadModel>>
{
    public Guid UserId { get; set; }
}