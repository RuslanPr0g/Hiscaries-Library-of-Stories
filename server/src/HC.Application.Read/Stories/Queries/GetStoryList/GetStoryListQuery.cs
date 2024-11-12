using HC.Application.Read.Stories.ReadModels;
using MediatR;

namespace HC.Application.Read.Stories.Queries;

public sealed class GetStoryListQuery : IRequest<IEnumerable<StorySimpleReadModel>>
{
    public Guid? Id { get; set; }
    public string SearchTerm { get; set; }
    public string Genre { get; set; }
    public string? RequesterUsername { get; set; }
    public Guid UserId { get; set; }
}