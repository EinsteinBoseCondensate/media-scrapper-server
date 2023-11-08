using Microsoft.Extensions.Logging;
using UserVideos.Application.Read.UserVideos;
using UserVideos.Domain;
using UserVideos.Domain.Interfaces;

namespace UserVideos.Application.Read.UserVideosByUser;
public class ReadUserVideosByUserService : ReadUserVideoServiceBase<ReadUserVideosByUserRequest, List<UserVideo>, ReadUserVideosByUserResponse>, IReadUserVideosByUserService
{
    private const string ErrorMessageScopeDeclarationString = "Error while getting user-videos";
    public ReadUserVideosByUserService(IUserVideoRepository userVideoRepository, ILogger<ReadUserVideoServiceBase<ReadUserVideosByUserRequest, List<UserVideo>, ReadUserVideosByUserResponse>> logger) : base(userVideoRepository, logger)
    {
    }

    protected override ReadUserVideosByUserResponse BuildSuccessResult(List<UserVideo> result, ReadUserVideosByUserRequest _)
    {
        return new ReadUserVideosByUserResponse()
        {
            Items = result
        };
    }

    protected override string ErrorMessageScopeDeclaration()
     => ErrorMessageScopeDeclarationString;

    protected override Task<List<UserVideo>> RepositoryAction(IUserVideoRepository repository, ReadUserVideosByUserRequest request)
    {
        return repository.GetAllAsync(userVideo => userVideo.UserId == request.UserId);
    }
}
