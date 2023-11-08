using YoutubeExplode.Search;

namespace Scrapping.Domain;

public class SearchItemResult : ISearchResult
{
    public string Url { get; set; }
    public string Title { get; set; }
    public string ThumbnailUrl { get; set; }
    public string DurationFormatted { get; set; }
    public string ChannelTitle { get; set; }

    public SearchItemResult(string? url, string? title, string? thumbnailUrl, string? durationFormatted, string? channelTitle)
    {
        Url = url ?? string.Empty;
        Title = title ?? string.Empty;
        ThumbnailUrl = thumbnailUrl ?? string.Empty;
        DurationFormatted = durationFormatted ?? string.Empty;
        ChannelTitle = channelTitle ?? string.Empty;
    }
}
