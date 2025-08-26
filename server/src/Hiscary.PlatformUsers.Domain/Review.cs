using StackNucleus.DDD.Domain;

namespace Hiscary.PlatformUsers.Domain;

/// <summary>
/// Represents a review of a <see cref="PlatformUser"/> about a library.
/// Only one review per person per library is allowed.
/// </summary>
public sealed class Review : Entity
{
    public Review(
        PlatformUserId userId,
        LibraryId libraryId,
        string message)
    {
        PlatformUserId = userId;
        LibraryId = libraryId;
        Message = message;
    }

    public PlatformUserId PlatformUserId { get; init; }
    public LibraryId LibraryId { get; init; }

    public string Message { get; private set; }

    internal void Edit(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            Message = message;
        }
    }

    private Review()
    {
    }
}