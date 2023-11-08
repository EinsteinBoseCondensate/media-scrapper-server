using Scrapping.Domain;
using YoutubeExplode.Common;
using YoutubeExplode.Search;

namespace Scrapping.Infrastructure.Extensions;
public static class Conversion
{
    public static SearchResult ToSearchResult(this PagedBatch<VideoSearchResult> result)
     => new SearchResult(result.Items.ToSearchItemResult(), result.ContinuationToken);

    public static IReadOnlyList<SearchItemResult> ToSearchItemResult(this IReadOnlyList<VideoSearchResult> results)
     => results.Select(ToSearchItemResult).ToList();

    public static SearchItemResult ToSearchItemResult(this VideoSearchResult result)
     => new SearchItemResult(
        result.EmbeddedVideoUrl(),
        result.Title,
        result.Thumbnails.FirstOrDefault()?.Url,
        result.Duration.ToString(),
        result.Author?.ChannelTitle);

    private static string EmbeddedVideoUrl(this VideoSearchResult result) => result.Url.Replace("watch?v=", "embed/");
}
