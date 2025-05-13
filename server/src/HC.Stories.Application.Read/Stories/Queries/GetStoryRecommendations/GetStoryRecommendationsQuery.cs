using HC.Stories.Application.Read.Stories.ReadModels;

namespace HC.Stories.Application.Read.Stories.Queries.GetStoryRecommendations;

public sealed class GetStoryRecommendationsQuery : IRequest<IEnumerable<StorySimpleReadModel>>
{
    public Guid UserId { get; set; }
}