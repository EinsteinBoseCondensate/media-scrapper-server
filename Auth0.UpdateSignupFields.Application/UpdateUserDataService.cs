using Amazon.Runtime.Internal.Util;
using Auth0.GetToken.Application.Extensions;
using Auth0.GetToken.Application.Interfaces;
using Auth0.ManagementApi;
using Auth0.UpdateSignupFields.Application.Extensions;
using Auth0.UpdateSignupFields.Application.Interfaces;
using Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Auth0.UpdateSignupFields.Application;
public class UpdateUserDataService : IUpdateUserDataService
{
    private IManagementApiClient? _managementApiClient;
    private readonly IGetTokenService _getTokenService;
    private readonly ILogger<UpdateUserDataService> _logger;
    public UpdateUserDataService(IGetTokenService getTokenService, ILogger<UpdateUserDataService> logger)
    {
        _getTokenService = getTokenService;
        _logger = logger;
    }

    public async Task<UpdateUserDataResponse> HandleAsync(UpdateUserDataRequest request)
    {
        try
        {
            _managementApiClient = await _getTokenService.UseToGetManagementApiClient().ConfigureAwait(false);

            var updateResponse = await _managementApiClient.Users.UpdateAsync(request.UserId, request.ToUserUpdateRequest()).ConfigureAwait(false);
            return new()
            {
                IsSuccess = true
            };
        }
        catch (Exception e)
        {
            _logger.LogError($"error while updating user details, content of exception was: {e.GetContent()}");
            return new();
        }
    }

}
