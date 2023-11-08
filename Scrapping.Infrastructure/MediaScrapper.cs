using Scrapping.Domain;
using Scrapping.Infrastructure.Extensions;
using YoutubeExplode;

namespace Scrapping.Infrastructure;
public class MediaScrapper : IMediaScrapper
{
    private readonly YoutubeClient _client;
    public MediaScrapper()
    {
        _client = new YoutubeClient();
    }

    public async Task<SearchResult> HandleAsync(SearchRequest request)
    {
        var result = await _client.Search.GetVideosPagedBatchAsync(request.UserInput!, request.ContinuationToken!);
        return result.ToSearchResult();
    }
}
