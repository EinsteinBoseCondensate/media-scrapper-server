using Microsoft.Extensions.Logging;
using UserVideos.Domain;
using UserVideos.Domain.Interfaces;

namespace UserVideos.Application.Read.SingleUserVideo;
public class ReadUserVideoService : ReadUserVideoServiceBase<ReadUserVideoRequest, UserVideo, ReadUserVideoResponse>, IReadUserVideoService
{
    private const string ErrorMessageScopeDeclarationString = "Error while getting user-video";
    public ReadUserVideoService(IUserVideoRepository userVideoRepository, ILogger<ReadUserVideoServiceBase<ReadUserVideoRequest, UserVideo, ReadUserVideoResponse>> logger) : base(userVideoRepository, logger)
    {
    }

    protected override ReadUserVideoResponse BuildSuccessResult(UserVideo result, ReadUserVideoRequest _)
    {
        return new ReadUserVideoResponse()
        {
            Item = result
        };
    }

    protected override string ErrorMessageScopeDeclaration()
     => ErrorMessageScopeDeclarationString;

    protected override Task<UserVideo> RepositoryAction(IUserVideoRepository repository, ReadUserVideoRequest request)
    {
        return repository.GetAsync(userVideo => userVideo.VideoId == request.VideoId && userVideo.UserId == request.UserId);
    }
}

