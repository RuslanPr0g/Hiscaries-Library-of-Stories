using Enterprise.Api.Rest.Requests;

namespace Hiscary.Stories.Api.Rest.Requests.Stories;

public sealed class GetStoryByIdsRequest
{
    public Guid[] Ids { get; set; }
    public SortableRequest Sorting { get; set; }
}
