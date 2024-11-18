using HC.Domain.PlatformUsers;

namespace HC.Application.Read.Users.ReadModels;

public class UserSimpleReadModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public static UserSimpleReadModel FromDomainModel(PlatformUser user)
    {
        return new()
        {
            Id = user.Id,
            Username = user.Username,
        };
    }
}