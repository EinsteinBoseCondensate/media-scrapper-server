using Common.Logging;
using MediaScrapper.Configuration;

namespace MediaScrapper.Extensions;

public static partial class IoC
{
    public static IServiceCollection AddServices(this IServiceCollection services, AppConfig appConfig)
    {
        return services.AddLoggerProvider()
            .AddConfigContracts(appConfig)
            .AddAuth0Services(appConfig)
            .AddScrappingServices()
            .AddUserVideosServices(appConfig);
    }
}