using HC.Domain.PlatformUsers;

namespace HC.Application.Read.Users.ReadModels;

public sealed class PlatformUserReadModel : UserSimpleReadModel
{
    public DateTime AccountCreated { get; set; }

    public new static PlatformUserReadModel FromDomainModel(PlatformUser user)
    {
        return new PlatformUserReadModel
        {
            Id = user.Id,
            Username = user.Username,
        };
    }
}
