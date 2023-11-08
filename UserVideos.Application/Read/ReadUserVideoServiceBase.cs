using Common.Contracts;
using Common.Extensions;
using Common.Models;
using Microsoft.Extensions.Logging;
using UserVideos.Domain.Interfaces;

namespace UserVideos.Application.Read;
public abstract class ReadUserVideoServiceBase<TRequest, TIntermediateResult, TResponse> :
    IOperationHandler<TRequest, TResponse>
    where TResponse : BaseResponse, new()
{
    private readonly IUserVideoRepository _userVideoRepository;
    private readonly ILogger<ReadUserVideoServiceBase<TRequest, TIntermediateResult, TResponse>> _logger;

    protected ReadUserVideoServiceBase(IUserVideoRepository userVideoRepository,
        ILogger<ReadUserVideoServiceBase<TRequest, TIntermediateResult, TResponse>> logger)
    {
        _userVideoRepository = userVideoRepository;
        _logger = logger;
    }

    protected abstract Task<TIntermediateResult> RepositoryAction(IUserVideoRepository repository, TRequest request);
    protected abstract TResponse BuildSuccessResult(TIntermediateResult result, TRequest request);
    protected abstract string ErrorMessageScopeDeclaration();

    public async Task<TResponse> HandleAsync(TRequest request)
    {
        try
        {
            var intermediateResult = await RepositoryAction(_userVideoRepository, request);
            var response = BuildSuccessResult(intermediateResult, request);
            response.IsSuccess = true;
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError($"{ErrorMessageScopeDeclaration()}\nRequest was: {request.Serialize()}\n Exception was: {ex.GetContent()}");
            return new();
        }
    }
}
