using MediaScrapper.Extensions;
using Microsoft.AspNetCore.Mvc;
using UserVideos.Application.Common;
using UserVideos.Application.Create;
using UserVideos.Application.Delete;
using UserVideos.Application.Read.UserVideos;
using UserVideos.Application.Read.UserVideosByUser;

namespace MediaScrapper.Endpoints;

public static class UserVideos
{
    public static Task<PersistenceResult> CreateAsync(
        [FromServices] ICreateUserVideoService createUserVideoService,
        [FromBody] CreateUserVideoRequest request,
        HttpContext context)
     => createUserVideoService.HandleAsync(request.PopulateUserIdFromHttpContext(context));

    public static Task<PersistenceResult> DeleteAsync(
        [FromServices] IDeleteUserVideoService deleteUserVideoService,
        [FromBody] DeleteUserVideoRequest request,
        HttpContext context)
     => deleteUserVideoService.HandleAsync(request.PopulateUserIdFromHttpContext(context));

    public static Task<ReadUserVideosByUserResponse> ReadByUserAsync(
        [FromServices] IReadUserVideosByUserService readUserVideosByUserService,
        HttpContext context)
     => readUserVideosByUserService.HandleAsync(new ReadUserVideosByUserRequest().PopulateUserIdFromHttpContext(context));

    private static TRequest PopulateUserIdFromHttpContext<TRequest>(this TRequest request, HttpContext context) 
        where TRequest : UserBasedUserVideoBaseRequest
    {
        request.UserId = context.ExtractUserId();
        return request;
    }
}
