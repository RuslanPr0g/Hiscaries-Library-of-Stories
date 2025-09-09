using Hiscary.Stories.Domain;

namespace Hiscary.Stories.Api.Rest.Requests.Stories;

public sealed record GetStoryByLibraryIdRequest
{
    public Guid LibraryId { get; set; }

    public StoryClientQueryableModelWithSortableRules QueryableModel { get; set; }
}