using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.FindFirst("username")?.Value;
    }
}
