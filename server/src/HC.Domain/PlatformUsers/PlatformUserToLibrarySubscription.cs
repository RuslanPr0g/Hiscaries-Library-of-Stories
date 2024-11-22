namespace HC.Domain.PlatformUsers;

public sealed class PlatformUserToLibrarySubscription : Entity
{
    public PlatformUserId PlatformUserId { get; set; }
    public PlatformUser PlatformUser { get; set; }

    public LibraryId LibraryId { get; set; }
    public Library Library { get; set; }
}
