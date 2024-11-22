namespace HC.Domain.PlatformUsers;

public sealed class PlatformUserToLibrarySubscription : Entity
{
    public PlatformUserToLibrarySubscription(PlatformUserId platformUserId, LibraryId libraryId)
    {
        PlatformUserId = platformUserId;
        LibraryId = libraryId;
    }

    public PlatformUserId PlatformUserId { get; }
    public PlatformUser PlatformUser { get; }

    public LibraryId LibraryId { get; }
    public Library Library { get; }
}
