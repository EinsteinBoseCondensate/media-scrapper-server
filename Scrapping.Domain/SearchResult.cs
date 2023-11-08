namespace Scrapping.Domain;
public class SearchResult
{
    public IReadOnlyList<SearchItemResult> Items { get; set; }
    public string? ContinuationToken { get; set; }

    public SearchResult(IReadOnlyList<SearchItemResult> items, string? continuationToken)
    {
        Items = items;
        ContinuationToken = continuationToken;
    }
}
