using HC.Domain.PlatformUsers;

namespace HC.Application.Read.Users.ReadModels;

public class LibraryReadModel
{
    public PlatformUserReadModel PlatformUser { get; set; }

    public Guid Id { get; set; }
    public string? Bio { get; private set; }
    public string? AvatarUrl { get; private set; }
    public List<string> LinksToSocialMedia { get; private set; } = [];

    public static LibraryReadModel FromDomainModel(Library library)
    {
        return new()
        {
            PlatformUser = PlatformUserReadModel.FromDomainModel(library.PlatformUser),
            Id = library.Id,
            Bio = library.Bio,
            AvatarUrl = library.AvatarUrl,
            LinksToSocialMedia = library.LinksToSocialMedia,
        };
    }
}