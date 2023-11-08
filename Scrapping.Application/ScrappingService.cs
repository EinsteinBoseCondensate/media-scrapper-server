using Common.Extensions;
using Microsoft.Extensions.Logging;
using Scrapping.Application.Extensions;
using Scrapping.Domain;

namespace Scrapping.Application;

public class ScrappingService : IScrappingService
{
    private readonly IMediaScrapper _mediaScrapper;
    private readonly ILogger<ScrappingService> _logger;
    public ScrappingService(IMediaScrapper mediaScrapper, ILogger<ScrappingService> logger)
    {
        _mediaScrapper = mediaScrapper;
        _logger = logger;
    }

    public async Task<ScrappingResult> HandleAsync(ScrappingRequest request)
    {
        try
        {
            if (string.IsNullOrEmpty(request.UserInput))
                throw new ArgumentNullException(nameof(request.UserInput));

            var result = await _mediaScrapper.HandleAsync(request.ToSearchRequest());

            return new(result)
            {
                IsSuccess = true,
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception at ScrappingService, it was: {ex.GetContent()}");
            return ScrappingResult.Default();
        }
    }
}
