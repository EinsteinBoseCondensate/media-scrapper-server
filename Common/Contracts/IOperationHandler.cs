namespace Common.Contracts;
public interface IOperationHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request);
}
public interface IOperationHandler<TResponse>
{
    Task<TResponse> HandleAsync();
}
public interface IOperationHandler
{
    Task HandleAsync();
}

