using HC.Application.Read.Stories.ReadModels;
using HC.Domain.Users;

namespace HC.Application.Read.Users.ReadModels;

public sealed class UserReadHistoryReadModel
{
    public IEnumerable<StoryWithProgressReadModel> Stories { get; set; }

    public static UserReadHistoryReadModel FromDomainModel(IEnumerable<UserReadHistory> history)
    {
        return new UserReadHistoryReadModel
        {
            Stories = history.Select(StoryWithProgressReadModel.FromDomainModel)
        };
    }
}