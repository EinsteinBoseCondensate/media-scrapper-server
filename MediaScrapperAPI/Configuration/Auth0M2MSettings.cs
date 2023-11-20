using Auth0.GetToken.Application.Config;
using Common.Extensions;

namespace MediaScrapper.Configuration;

public class Auth0M2MSettings : IAuth0M2MSettings
{
    public string? ClientId { get; set; }
    public string? ServerDomain { get; set; }
    public string? Audience { get; set; }
    public string? ClientSecret { get; set; }
    public Auth0M2MSettings()
    {
        ClientSecret = Constants.M2M_CLIENT_SECRET.GetEnvironmentVariableBasedOnOperativeSystem();
    }
}
