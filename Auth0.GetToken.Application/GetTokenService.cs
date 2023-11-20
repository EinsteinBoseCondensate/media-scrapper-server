using Amazon.Runtime.Internal.Util;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.GetToken.Application.Config;
using Auth0.GetToken.Application.Interfaces;
using Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Auth0.GetToken.Application;

public class GetTokenService : IGetTokenService
{
    private readonly IAuthenticationApiClient _authenticationApiClient;
    private static AccessTokenResponse? _accessTokenResponse;
    private static long? _accessTokenResponseRetrievalTicks;
    private readonly ILogger<GetTokenService> _logger;
    public IAuth0M2MSettings Auth0M2MSettings { get; private set; }
    public GetTokenService(IAuthenticationApiClient authenticationApiClient, IAuth0M2MSettings auth0M2MSettings, ILogger<GetTokenService> logger)
    {
        _authenticationApiClient = authenticationApiClient;
        Auth0M2MSettings = auth0M2MSettings;
        _logger = logger;
    }
    public async Task<string> HandleAsync()
    {
        if (IsCachedAccessTokenResponseValid())
        {
            return _accessTokenResponse!.AccessToken;
        }

        try
        {
            _logger.LogDebug($"Config: {Auth0M2MSettings.Serialize()}");
            _accessTokenResponse = await _authenticationApiClient.GetTokenAsync(new ClientCredentialsTokenRequest()
            {
                Audience = Auth0M2MSettings.Audience,
                ClientId = Auth0M2MSettings.ClientId,
                ClientSecret = Auth0M2MSettings.ClientSecret
            }).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            _logger.LogError($"Error grabbing accessTokenResponse, exception content was: {e}");
            throw;
        }
        _accessTokenResponseRetrievalTicks = DateTime.Now.Ticks;
        return _accessTokenResponse.AccessToken;
    }

    private static bool IsCachedAccessTokenResponseValid()
    {
        return _accessTokenResponse != null && _accessTokenResponseRetrievalTicks != null &&
            (DateTime.Now.Ticks - _accessTokenResponseRetrievalTicks)/10000 < _accessTokenResponse.ExpiresIn;
    }
}
