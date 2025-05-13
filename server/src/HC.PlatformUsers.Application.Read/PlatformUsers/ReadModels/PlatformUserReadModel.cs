namespace HC.Application.Read.Users.ReadModels;

public class PlatformUserReadModel
{
    public Guid Id { get; set; }
    public string Username { get; set; }

    public static PlatformUserReadModel FromDomainModel(PlatformUser user)
    {
        return new()
        {
            Id = user.Id,
            Username = user.Username,
        };
    }
}
