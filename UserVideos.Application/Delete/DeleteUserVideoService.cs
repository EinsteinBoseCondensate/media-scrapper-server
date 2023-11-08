using Microsoft.Extensions.Logging;
using UserVideos.Application.Common;
using UserVideos.Domain.Interfaces;

namespace UserVideos.Application.Delete;
public class DeleteUserVideoService : CreateDeleteUserVideosServiceBase<DeleteUserVideoRequest>, IDeleteUserVideoService
{
    private const string ErrorMessageScopeDeclarationString = "Error while deleting user-video";

    public DeleteUserVideoService(IUserVideoRepository userVideoRepository, ILogger<CreateDeleteUserVideosServiceBase<DeleteUserVideoRequest>> logger) : base(userVideoRepository, logger) { }

    protected override string ErrorMessageScopeDeclaration()
     => ErrorMessageScopeDeclarationString;

    protected override async Task RepositoryAction(IUserVideoRepository repository, DeleteUserVideoRequest request)
    {
        var count = await repository.CountAsync(userVideo => userVideo.Id == request.Id && userVideo.UserId == request.UserId);
        if (count == 0)
            throw new ArgumentException($"Intended to delete video with Id: {request.Id}, doesn't belong to user: {request.UserId}");

        await repository.RemoveAsync(request.Id);
    }
}
