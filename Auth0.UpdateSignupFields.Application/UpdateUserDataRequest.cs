using Common.Models;

namespace Auth0.UpdateSignupFields.Application;
public class UpdateUserDataRequest : UserIdBasedRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
