using Scrapping.Domain;

namespace Scrapping.Application.Extensions;
public static class Conversion
{
    public static SearchRequest ToSearchRequest(this ScrappingRequest scrappingRequest) => new()
    {
        UserInput = scrappingRequest.UserInput,
        ContinuationToken = scrappingRequest.ContinuationToken
    };
}
