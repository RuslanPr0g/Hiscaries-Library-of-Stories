using HC.Domain.Users;

namespace HC.Application.Read.Users.ReadModels;

public class UserSimpleReadModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public static UserSimpleReadModel FromDomainModel(User user)
    {
        return new()
        {
            Id = user.Id,
            Username = user.Username,
        };
    }
}