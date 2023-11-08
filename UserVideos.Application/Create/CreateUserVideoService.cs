using Microsoft.Extensions.Logging;
using UserVideos.Application.Common;
using UserVideos.Domain.Interfaces;

namespace UserVideos.Application.Create;
public class CreateUserVideoService : CreateDeleteUserVideosServiceBase<CreateUserVideoRequest>, ICreateUserVideoService
{
    private const string ErrorMessageScopeDeclarationString = "Error while creating user-video";
    public CreateUserVideoService(IUserVideoRepository userVideoRepository, ILogger<CreateDeleteUserVideosServiceBase<CreateUserVideoRequest>> logger) : base(userVideoRepository, logger)
    {
    }

    protected override string ErrorMessageScopeDeclaration()
     => ErrorMessageScopeDeclarationString;

    protected override Task RepositoryAction(IUserVideoRepository repository, CreateUserVideoRequest request)
     => repository.CreateAsync(request.ToUserVideo());
}
