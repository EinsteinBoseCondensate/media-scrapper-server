
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Common.Extensions;

public static class Authentication
{
    public static string? ExtractUserId(this HttpContext context) => context.ExtractClaimValue(ClaimTypes.NameIdentifier);
    private static string? ExtractClaimValue(this HttpContext context, string claimType) => context.User.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
}
