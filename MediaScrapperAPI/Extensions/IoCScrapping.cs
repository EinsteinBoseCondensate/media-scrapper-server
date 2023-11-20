using Scrapping.Application;
using Scrapping.Domain;

namespace MediaScrapper.Extensions;

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
