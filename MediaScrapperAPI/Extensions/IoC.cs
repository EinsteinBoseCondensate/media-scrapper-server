using Auth0.AuthenticationApi;
using Common.Logging;
using MediaScrapper.Authentication;
using MediaScrapper.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Scrapping.Application;
using Scrapping.Domain;
using System.Security.Claims;
using System.Text;
using UserVideos.Application.Create;
using UserVideos.Application.Delete;
using UserVideos.Application.Read.UserVideosByUser;
using UserVideos.Domain.Interfaces;
using UserVideos.Infrastructure;

namespace MediaScrapper.Extensions;

public static partial class IoC
{
    public static IServiceCollection AddServices(this IServiceCollection services, AppConfig appConfig)
    {
        return services.AddLoggerProvider()
            .AddAuth0Services(appConfig)
            .AddScrappingServices()
            .AddUserVideosServices(appConfig);
    }

    public static void AddAppAuthorizationPolicies(this IServiceCollection serviceCollection, AppConfig appConfig)
    {
        serviceCollection
            .AddAuthentication()
            .AddJwtBearer(Auth0AuthenticationExtensionAuthorizationPolicy.RelatedAuthenticationScheme, options =>
            {
                var issuerSigningKeyFromEnvironment = OperatingSystem.IsWindows() ? 
                    Environment.GetEnvironmentVariable(Constants.ASPNETCORE_ENVIRONMENT) == Environments.Development ?
                        Environment.GetEnvironmentVariable(Constants.CUSTOM_SIGNUP_FIELDS_ACTION_SECRETS_JWT_SIGNING_KEY_VALUE, EnvironmentVariableTarget.User) :
                        Environment.GetEnvironmentVariable(Constants.CUSTOM_SIGNUP_FIELDS_ACTION_SECRETS_JWT_SIGNING_KEY_VALUE, EnvironmentVariableTarget.Machine) :
                Environment.GetEnvironmentVariable(Constants.CUSTOM_SIGNUP_FIELDS_ACTION_SECRETS_JWT_SIGNING_KEY_VALUE);

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
public static partial class IoC
{
    private static IServiceCollection AddMongoSpecificServices(this IServiceCollection services, AppConfig appConfig)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        services.AddSingleton(serviceProvider =>
        {
            var mongoClient = new MongoClient(appConfig.MongoDbSettings?.ConnectionString ?? throw new Exception("Bad config!"));
            return mongoClient.GetDatabase(appConfig.MongoDbSettings.UserVideosDatabaseName);
        });

        return services;
    }

    private static IServiceCollection AddUserVideosInfrastructure(this IServiceCollection services, AppConfig appConfig)
    {
        services.AddMongoSpecificServices(appConfig);
        services.AddScoped<IUserVideoRepository, UserVideosRepository>();
        return services;
    }

    private static IServiceCollection AddUserVideosServices(this IServiceCollection services, AppConfig appConfig)
    {
        return services.AddUserVideosInfrastructure(appConfig)
            .AddUserVideosApplicationServices();
    }
    private static IServiceCollection AddUserVideosApplicationServices(this IServiceCollection services)
    {
        return services.AddScoped<ICreateUserVideoService, CreateUserVideoService>()
            .AddScoped<IDeleteUserVideoService, DeleteUserVideoService>()
            .AddScoped<IReadUserVideosByUserService, ReadUserVideosByUserService>();
    }
}

public static partial class IoC
{
    public static IServiceCollection AddScrappingServices(this IServiceCollection services)
    {
        //Application
        return services.AddScoped<IScrappingService, ScrappingService>()
        //Infrastructure
        .AddScoped<IMediaScrapper, Scrapping.Infrastructure.MediaScrapper>();
    }
}

public static partial class IoC
{
    public static IServiceCollection AddAuth0Services(this IServiceCollection services, AppConfig appConfig)
    {
        return services.AddSingleton<IAuthenticationApiClient>(_ => new AuthenticationApiClient(appConfig.Auth0SpaSettings?.ServerDomain));
    }
}