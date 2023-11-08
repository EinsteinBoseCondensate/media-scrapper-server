using System.Security.Claims;

namespace MediaScrapper.Extensions;

public static class Authentication
{
    public static string? ExtractUserId(this HttpContext context) => context.ExtractClaimValue(ClaimTypes.NameIdentifier);
    private static string? ExtractClaimValue(this HttpContext context, string claimType) => context.User.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
}
