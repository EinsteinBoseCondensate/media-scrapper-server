using System.Collections.Generic;

namespace YoutubeExplode.Common;

/// <summary>
/// Additional batch representation to hold the continuationToken of a given search
/// </summary>
public class PagedBatch<T> : Batch<T>
    where T : IBatchItem
{
    /// <summary>
    /// Token used by youtube to handle pagination
    /// </summary>
    public string? ContinuationToken { get; set; }

    /// <summary>
    /// Initializes paged batch
    /// </summary>
    /// <param name="items"></param>
    /// <param name="continuationToken"></param>
    public PagedBatch(IReadOnlyList<T> items, string? continuationToken)
        : base(items)
    {
        ContinuationToken = continuationToken;
    }
}

/// <summary>
/// Factory
/// </summary>
public static class PagedBatch
{
    /// <summary>
    /// Static method, mostly for aesthetics
    /// </summary>
    /// <param name="items"></param>
    /// <param name="continuationToken"></param>
    /// <returns></returns>
    public static PagedBatch<T> Create<T>(IReadOnlyList<T> items, string? continuationToken)
        where T : IBatchItem => new(items, continuationToken);
}
