using Auth0.UpdateSignupFields.Application;
using Auth0.UpdateSignupFields.Application.Interfaces;
using Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Scrapping.Application;

namespace MediaScrapper.Endpoints;

public static class Auth0
{
    public static Task<UpdateUserDataResponse> UpdateAsync(
        [FromServices] IUpdateUserDataService updateUserDataService,
        [FromBody] UpdateUserDataRequest scrappingRequest,
        HttpContext context)
     => updateUserDataService.HandleAsync(scrappingRequest.PopulateUserIdFromHttpContext(context));
}
