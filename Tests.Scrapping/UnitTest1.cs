using Microsoft.Extensions.DependencyInjection;
using Scrapping.Application;
using Tests.Scrapping.IoC;
using YoutubeExplode.Search;

namespace Tests.Scrapping;

public class Tests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IScrappingService _scrappingService;
    public Tests()
    {
        _serviceProvider = Bootstrapping.InjectServicesAndBuildServiceProvider();
        _scrappingService = _serviceProvider.GetRequiredService<IScrappingService>();
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task EnsureScrappingSearchResponseNotNull()
    {
        var request = new ScrappingRequest()
        {
            UserInput = "stephan bodzin"
        };
        var results = await _scrappingService.HandleAsync(request);
        Assert.IsNotNull(results);
    }

    [Test]
    public async Task EnsureScrappingSearchResponsePaginationWorks()
    {
        var searchClient = new SearchClient(new HttpClient());
        var request = new ScrappingRequest()
        {
            UserInput = "stephan bodzin"
        };
        var results = await searchClient.GetVideosPagedBatchAsync(request.UserInput, request.ContinuationToken);
        request.ContinuationToken = results.ContinuationToken;

        var results2 = await searchClient.GetVideosPagedBatchAsync(request.UserInput, request.ContinuationToken);
        request.ContinuationToken = results2.ContinuationToken;

        var results3 = await searchClient.GetVideosPagedBatchAsync(request.UserInput, request.ContinuationToken);
        Assert.IsNotNull(results3);
    }
}