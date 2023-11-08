using Common.Models;
using Scrapping.Domain;

namespace Scrapping.Application;
public class ScrappingResult : BaseResponse
{
    public SearchResult Result { get; set; }

    public ScrappingResult(SearchResult result)
    {
        Result = result;
    }
    public static ScrappingResult Default() => new(new(Array.Empty<SearchItemResult>(), string.Empty));
}
