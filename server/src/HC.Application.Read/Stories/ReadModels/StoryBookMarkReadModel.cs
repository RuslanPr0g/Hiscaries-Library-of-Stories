using HC.Domain.Users;

namespace HC.Application.Read.Stories.ReadModels;

public sealed class StoryBookMarkReadModel
{
    public Guid StoryId { get; set; }
    public DateTime CreatedAt { get; set; }

    public static StoryBookMarkReadModel FromDomainModel(UserStoryBookMark bookmark)
    {
        return new StoryBookMarkReadModel
        {
            StoryId = bookmark.StoryId,
            CreatedAt = bookmark.CreatedAt,
        };
    }
}