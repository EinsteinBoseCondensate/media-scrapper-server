using Common.Contracts;

namespace Scrapping.Application;

public interface IScrappingService : IOperationHandler<ScrappingRequest, ScrappingResult>
{
}
