using Auth0.GetToken.Application.Interfaces;
using Auth0.ManagementApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth0.GetToken.Application.Extensions;
public static class Auth0Extensions
{
    public static async Task<IManagementApiClient> UseToGetManagementApiClient(this IGetTokenService service)
    {
        var accessToken = await service.HandleAsync().ConfigureAwait(false);
        return new ManagementApiClient(accessToken, new Uri(service.Auth0M2MSettings?.Audience ?? throw new Exception("missing audience from Auth0M2MSettings")));        
    }
}
