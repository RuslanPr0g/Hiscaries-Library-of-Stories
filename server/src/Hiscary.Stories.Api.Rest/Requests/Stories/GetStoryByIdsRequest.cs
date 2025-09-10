using Hiscary.Stories.Domain;

namespace Hiscary.Stories.Api.Rest.Requests.Stories;

public sealed record GetStoryByIdsRequest
{
    public Guid[] Ids { get; set; }

    public StoryClientQueryableModelWithSortableRules QueryableModel { get; set; }
}
