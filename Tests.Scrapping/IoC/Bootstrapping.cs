using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scrapping.Application;
using Scrapping.Domain;
using Scrapping.Infrastructure;
using Tests.Scrapping.Mocks;

namespace Tests.Scrapping.IoC;
public static class Bootstrapping
{
    public static IServiceProvider InjectServicesAndBuildServiceProvider()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IScrappingService, ScrappingService>();
        serviceCollection.AddSingleton<IMediaScrapper, MediaScrapper>();
        serviceCollection.AddSingleton<ILogger<ScrappingService>>(_ => new GenericLogger<ScrappingService>());

        return serviceCollection.BuildServiceProvider();
    }
}
