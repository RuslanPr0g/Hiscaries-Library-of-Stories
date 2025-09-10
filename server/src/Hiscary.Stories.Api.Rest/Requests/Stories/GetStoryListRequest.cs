using Hiscary.Stories.Domain;

namespace Hiscary.Stories.Api.Rest.Requests.Stories;

public sealed record GetStoryListRequest
{
    public Guid Id { get; set; }

    public string SearchTerm { get; set; }

    public string Genre { get; set; }

    public StoryClientQueryableModelWithSortableRules QueryableModel { get; set; }
}
