using System.Security.Claims;

namespace Enterprise.Api.Rest;

public sealed record CurrentUser
{
    public Guid? Id { get; }
    public string? Username { get; }

    public bool HasNoData { get; init; } = true;

    private CurrentUser(Guid? id, string? username)
    {
        Id = id;
        Username = username;

        if (Id is null && string.IsNullOrWhiteSpace(username))
        {
            HasNoData = true;
        }
        else
        {
            HasNoData = false;
        }
    }

    public static CurrentUser FromClaims(ClaimsPrincipal user)
    {
        var id = user.GetUserId();
        var username = user.GetUsername();
        return new CurrentUser(id, username);
    }
}
