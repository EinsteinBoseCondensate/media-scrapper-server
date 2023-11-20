using Auth0.GetToken.Application.Config;
using Common.Extensions;
using MediaScrapper.Configuration;

namespace MediaScrapper.Extensions;

public static partial class IoC
{
    public static IServiceCollection AddConfigContracts(this IServiceCollection services, AppConfig appConfig)
    {
        if(appConfig.Auth0M2MSettings == null)
            throw new Exception("missing auth0-m2m-settings config node");

        appConfig.Auth0M2MSettings.ClientSecret = Constants.M2M_CLIENT_SECRET.GetEnvironmentVariableBasedOnOperativeSystem();
        return services.AddSingleton<IAuth0M2MSettings>(_ => appConfig.Auth0M2MSettings);
    }
}
