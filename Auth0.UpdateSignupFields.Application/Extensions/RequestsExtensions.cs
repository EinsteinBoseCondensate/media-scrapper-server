using Auth0.ManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth0.UpdateSignupFields.Application.Extensions;
internal static class RequestsExtensions
{
    internal static UserUpdateRequest ToUserUpdateRequest(this UpdateUserDataRequest request)
        => new()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
}
