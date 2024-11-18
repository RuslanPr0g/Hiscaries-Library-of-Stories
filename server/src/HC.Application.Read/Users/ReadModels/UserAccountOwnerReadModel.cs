using HC.Domain.Users;

namespace HC.Application.Read.Users.ReadModels;

public sealed class UserAccountOwnerReadModel : UserSimpleReadModel
{
    public DateTime AccountCreated { get; set; }

    public static new UserAccountOwnerReadModel FromDomainModel(User user)
    {
        return new UserAccountOwnerReadModel
        {
            Id = user.Id,
            Username = user.Username,
            AccountCreated = user.CreatedAt,
        };
    }
}
