using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.GetToken.Application.Config;
using Auth0.GetToken.Application.Interfaces;

namespace Auth0.GetToken.Application;

public class GetTokenService : IGetTokenService
{
    private readonly IAuthenticationApiClient _authenticationApiClient;
    private static AccessTokenResponse? _accessTokenResponse;
    private readonly static long? _accessTokenResponseRetrievalTicks;
    public IAuth0M2MSettings Auth0M2MSettings { get; private set; }
    public GetTokenService(IAuthenticationApiClient authenticationApiClient, IAuth0M2MSettings auth0M2MSettings)
    {
        _authenticationApiClient = authenticationApiClient;
        Auth0M2MSettings = auth0M2MSettings;
    }
    public async Task<string> HandleAsync()
    {
        if (IsCachedAccessTokenResponseValid())
        {
            return _accessTokenResponse!.AccessToken;
        }

        _accessTokenResponse = await _authenticationApiClient.GetTokenAsync(new ClientCredentialsTokenRequest()
        {
            Audience = Auth0M2MSettings.Audience,
            ClientId = Auth0M2MSettings.ClientId,
            ClientSecret = Auth0M2MSettings.ClientSecret
        }).ConfigureAwait(false);

        return _accessTokenResponse.AccessToken;
    }

    private static bool IsCachedAccessTokenResponseValid()
    {
        return _accessTokenResponse != null && _accessTokenResponseRetrievalTicks != null &&
            DateTime.Now.Ticks - _accessTokenResponseRetrievalTicks < _accessTokenResponse.ExpiresIn;
    }
}
