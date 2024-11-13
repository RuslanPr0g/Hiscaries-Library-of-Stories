using HC.Application.Read.Reviews.ReadModels;
using HC.Application.Read.Stories.ReadModels;
using HC.Domain.Users;

namespace HC.Application.Read.Users.ReadModels;

public sealed class UserAccountOwnerReadModel : UserSimpleReadModel
{
    public DateTime AccountCreated { get; set; }
    public IEnumerable<StoryBookMarkReadModel> BookmarkedStories { get; set; }
    public IEnumerable<ReviewReadModel> Reviews { get; set; }

    public static new UserAccountOwnerReadModel FromDomainModel(User user)
    {
        return new UserAccountOwnerReadModel
        {
            Id = user.Id,
            Username = user.Username,
            AccountCreated = user.CreatedAt,
            BirthDate = user.BirthDate,
            Age = DateTime.UtcNow.Year - user.BirthDate.Year,
            BookmarkedStories = user.BookMarks.Select(StoryBookMarkReadModel.FromDomainModel),
            Reviews = user.Reviews.Select(x =>
                ReviewReadModel.FromDomainModel(x,
                UserSimpleReadModel.FromDomainModel(user),
                UserSimpleReadModel.FromDomainModel(user))),
        };
    }
}
