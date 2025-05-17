using System.Security.Claims;

namespace Enterprise.Api.Rest;

public sealed record CurrentUser
{
    public Guid? Id { get; }
    public string? Username { get; }

    public bool IsIdEmpty => !Id.HasValue;
    public bool IsUsernameEmpty => string.IsNullOrWhiteSpace(Username);
    public bool IsUserEmpty => IsIdEmpty && IsUsernameEmpty;

    private CurrentUser(Guid? id, string? username)
    {
        Id = id;
        Username = username;
    }

    public static CurrentUser FromClaims(ClaimsPrincipal user)
    {
        var id = user.GetUserId();
        var username = user.GetUsername();
        return new CurrentUser(id, username);
    }
}
