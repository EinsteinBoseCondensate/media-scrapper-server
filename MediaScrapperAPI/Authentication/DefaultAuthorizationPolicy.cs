using Auth0.AuthenticationApi;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MediaScrapper.Authentication;

public static class DefaultAuthorizationPolicy
{
    public const string Name = nameof(DefaultAuthorizationPolicy);
    public static async Task<bool> Delegate(AuthorizationHandlerContext context)
    {
        var httpContext = context.Resource as HttpContext ?? throw new Exception();

        if (!httpContext.TryGetAccessToken(out var accessToken))
        {
            return false;
        }

        if (AuthorizationCache.AccessTokensToUserIds.TryGetValue(accessToken, out var userId))
        {
            httpContext.AddUserIdToClaims(userId);
            return true;
        }

        var authenticationApiClient = httpContext.RequestServices.GetRequiredService<IAuthenticationApiClient>();

        var userInfo = await authenticationApiClient.GetUserInfoAsync(accessToken);

        if (userInfo == null)
        {
            return false;
        }

        httpContext.AddUserIdToClaims(userInfo.UserId);

        AuthorizationCache.AccessTokensToUserIds.TryAdd(accessToken, userInfo.UserId);

        return true;

    }

    private static bool TryGetAccessToken(this HttpContext httpContext, out string accessToken)
    {
        accessToken = string.Empty;
        if (!httpContext.Request.Headers.TryGetValue("Authorization", out var bearerHeaderValue))
        {
            return false;
        }

        var headerArray = bearerHeaderValue.ToString().Split(" ");

        if (headerArray.Length != 2)
        {
            return false;
        }

        accessToken = headerArray[1];
        return true;
    }

    private static void AddUserIdToClaims(this HttpContext httpContext, string userId)
        => ((ClaimsIdentity)httpContext.User.Identity!).AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
}
