using Common.Models;
using Microsoft.AspNetCore.Http;

namespace Common.Extensions;
public static class RequestsExtensions
{
    public static TRequest PopulateUserIdFromHttpContext<TRequest>(this TRequest request, HttpContext context)
    where TRequest : UserIdBasedRequest
    {
        request.UserId = context.ExtractUserId();
        return request;
    }
}
