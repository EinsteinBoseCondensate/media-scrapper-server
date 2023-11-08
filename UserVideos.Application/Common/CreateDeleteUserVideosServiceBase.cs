using Common.Extensions;
using Microsoft.Extensions.Logging;
using UserVideos.Domain.Interfaces;

namespace UserVideos.Application.Common;
public abstract class CreateDeleteUserVideosServiceBase<TRequest> : ICreateDeleteUserVideosService<TRequest>
{
    private readonly IUserVideoRepository _userVideoRepository;
    private readonly ILogger<CreateDeleteUserVideosServiceBase<TRequest>> _logger;

    protected CreateDeleteUserVideosServiceBase(IUserVideoRepository userVideoRepository, ILogger<CreateDeleteUserVideosServiceBase<TRequest>> logger)
    {
        _userVideoRepository = userVideoRepository;
        _logger = logger;
    }

    protected abstract Task RepositoryAction(IUserVideoRepository repository, TRequest request);
    protected abstract string ErrorMessageScopeDeclaration();

    public async Task<PersistenceResult> HandleAsync(TRequest request)
    {
        try
        {
            await RepositoryAction(_userVideoRepository, request);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{ErrorMessageScopeDeclaration()}\nRequest was: {request.Serialize()}\n Exception was: {ex.GetContent()}");
            return new();
        }
        return new()
        {
            IsSuccess = true,
        };
    }
}
