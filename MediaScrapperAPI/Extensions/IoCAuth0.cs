using Auth0.AuthenticationApi;
using Auth0.GetToken.Application;
using Auth0.GetToken.Application.Interfaces;
using Auth0.ManagementApi;
using Auth0.UpdateSignupFields.Application;
using Auth0.UpdateSignupFields.Application.Interfaces;
using MediaScrapper.Configuration;

namespace MediaScrapper.Extensions;

public static partial class IoC
{
    public static IServiceCollection AddAuth0Services(this IServiceCollection services, AppConfig appConfig)
    {
        return services.AddSingleton<IAuthenticationApiClient>(_ => new AuthenticationApiClient(appConfig.Auth0SpaSettings?.ServerDomain))
            .AddScoped<IGetTokenService, GetTokenService>()
            .AddScoped<IUpdateUserDataService, UpdateUserDataService>();
    }
}
