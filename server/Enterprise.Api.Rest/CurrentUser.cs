using System.Security.Claims;

namespace Enterprise.Api.Rest;

public sealed record CurrentUser
{
    public Guid Id { get; }
    public string Username { get; }

    private CurrentUser(Guid id, string username)
    {
        Id = id;
        Username = username;
    }

    public static CurrentUser? FromClaims(ClaimsPrincipal user)
    {
        var id = user.GetUserId();
        var username = user.GetUsername();

        if (!id.HasValue || string.IsNullOrWhiteSpace(username))
        {
            return null;
        }

        return new CurrentUser(id.Value, username);
    }
}
