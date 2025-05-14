using HC.Stories.Application.Read.Stories.ReadModels;

namespace HC.Stories.Application.Read.Services.GetStoryList;

public sealed class GetStoryListQuery : IRequest<IEnumerable<StorySimpleReadModel>>
{
    public Guid? Id { get; set; }
    public Guid? LibraryId { get; set; }
    public string SearchTerm { get; set; }
    public string Genre { get; set; }
    public string? RequesterUsername { get; set; }
    public Guid UserId { get; set; }
}