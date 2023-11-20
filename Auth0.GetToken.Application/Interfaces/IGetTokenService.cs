using Auth0.GetToken.Application.Config;
using Common.Contracts;

namespace Auth0.GetToken.Application.Interfaces;
public interface IGetTokenService : IOperationHandler<string>
{
    IAuth0M2MSettings Auth0M2MSettings { get; }
}
