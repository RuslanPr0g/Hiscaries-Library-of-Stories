using System.Collections.Generic;

namespace HC.Domain.PlatformUsers;

/// <summary>
/// Represents a collection of stories published by a <see cref="PlatformUser"/>. Each story
/// is linked to a single library, and the library belongs exclusively to the 
/// <see cref="PlatformUser"/> who created it.
/// </summary>
public sealed class Library : Entity<LibraryId>
{
    public Library(
        LibraryId id,
        PlatformUserId userId) : base(id)
    {
        PlatformUserId = userId;
    }

    public PlatformUserId PlatformUserId { get; init; }
    public PlatformUser PlatformUser { get; init; }

    public string? Bio { get; private set; }
    public string? AvatarUrl { get; private set; }
    public List<string> LinksToSocialMedia { get; private set; } = [];

    public void Edit(string? bio, string? avatarUrl, List<string> linksToSocialMedia)
    {
        Bio = bio;
        AvatarUrl = avatarUrl;
        LinksToSocialMedia = linksToSocialMedia;
    }

    private Library()
    {
    }
}