using Common.Contracts;

namespace Auth0.UpdateSignupFields.Application.Interfaces;
public interface IUpdateUserDataService : IOperationHandler<UpdateUserDataRequest, UpdateUserDataResponse>
{
}
