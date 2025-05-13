using HC.Stories.Application.Read.Stories.ReadModels;

namespace HC.Stories.Application.Read.Stories.Queries.GetStoryReadingHistory;

public sealed class GetStoryReadingHistoryQuery : IRequest<IEnumerable<StorySimpleReadModel>>
{
    public Guid UserId { get; set; }
}