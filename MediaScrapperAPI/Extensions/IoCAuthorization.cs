using Common.Extensions;
using MediaScrapper.Authentication;
using MediaScrapper.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MediaScrapper.Extensions;


public static partial class IoC
{
    public static void AddAppAuthorizationPolicies(this IServiceCollection serviceCollection, AppConfig appConfig)
    {
        serviceCollection
            .AddAuthentication()
            .AddJwtBearer(Auth0AuthenticationExtensionAuthorizationPolicy.RelatedAuthenticationScheme, options =>
            {
                var issuerSigningKeyFromEnvironment = Constants.CUSTOM_SIGNUP_FIELDS_ACTION_SECRETS_JWT_SIGNING_KEY_VALUE.GetEnvironmentVariableBasedOnOperativeSystem();

                if (string.IsNullOrEmpty(issuerSigningKeyFromEnvironment))
                    throw new Exception("Issuer signing key for custom signup process client was null while grabbing it from Environment.");

                options.Authority = appConfig.Auth0SpaSettings?.Issuer ??
                throw new Exception("Issuer for custom signup process client was null while grabbing it from AppConfig.");

                options.TokenValidationParameters = new()
                {
                    NameClaimType = ClaimTypes.NameIdentifier,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKeyFromEnvironment)),
                    IssuerValidator = (issuer, _, _) => issuer == options.Authority ? issuer : options.Authority?.Replace("https://", "")
                };
            });

        serviceCollection
            .AddAuthorization(options =>
            {
                options.AddPolicy(Auth0AuthenticationExtensionAuthorizationPolicy.Name,
                    options =>
                    {
                        options.AuthenticationSchemes = new[] { Auth0AuthenticationExtensionAuthorizationPolicy.RelatedAuthenticationScheme };
                        options.RequireAuthenticatedUser();
                    });
                options.AddPolicy(DefaultAuthorizationPolicy.Name, options =>
                {
                    options.RequireAssertion(DefaultAuthorizationPolicy.Delegate);
                });
            });
    }
}
