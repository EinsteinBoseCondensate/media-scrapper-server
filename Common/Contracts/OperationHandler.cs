namespace Common.Contracts;
public interface IOperationHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request);
}
