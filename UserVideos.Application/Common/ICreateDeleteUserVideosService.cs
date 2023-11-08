using Common.Contracts;

namespace UserVideos.Application.Common;
public interface ICreateDeleteUserVideosService<TRequest> : IOperationHandler<TRequest, PersistenceResult>
{
}
