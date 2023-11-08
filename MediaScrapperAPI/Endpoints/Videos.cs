using Microsoft.AspNetCore.Mvc;
using Scrapping.Application;

namespace MediaScrapper.Endpoints;

public static class Videos
{
    public static Task<ScrappingResult> GetAsync(
        [FromServices] IScrappingService scrappingService,
        [FromBody] ScrappingRequest scrappingRequest)
     => scrappingService.HandleAsync(scrappingRequest);
}
