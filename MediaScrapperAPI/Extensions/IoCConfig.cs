using Auth0.GetToken.Application.Config;
using MediaScrapper.Configuration;

namespace MediaScrapper.Extensions;

public static partial class IoC
{
    public static IServiceCollection AddConfigContracts(this IServiceCollection services, AppConfig appConfig)
    {
        return services.AddSingleton<IAuth0M2MSettings>(_ => appConfig.Auth0M2MSettings ?? throw new Exception("missing auth0-m2m-settings config node"));
    }
}
