using Common.Contracts;

namespace Scrapping.Domain;
public interface IMediaScrapper : IOperationHandler<SearchRequest, SearchResult>
{
}
