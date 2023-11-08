using System.Collections.Concurrent;

namespace MediaScrapper.Authentication;

public static class AuthorizationCache
{
    //Not really correct, we don't have timestamps here
    public static readonly ConcurrentDictionary<string, string> AccessTokensToUserIds = new ConcurrentDictionary<string, string>();
}
