using HC.Stories.Application.Read.Stories.ReadModels;

namespace HC.Stories.Application.Read.Services.GetStoryWithContents;

public sealed class GetStoryWithContentsQuery : IRequest<StoryWithContentsReadModel?>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}