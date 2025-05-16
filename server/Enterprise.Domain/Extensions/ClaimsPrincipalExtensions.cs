using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUsername(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst("username");
        if (claim is null)
        {
            return null;
        }

        return claim.Value;
    }

    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst("id");
        if (claim is null || !Guid.TryParse(claim.Value, out Guid value))
        {
            return null;
        }

        return value;
    }
}
