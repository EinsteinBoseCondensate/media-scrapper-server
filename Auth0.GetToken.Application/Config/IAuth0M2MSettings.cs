namespace Auth0.GetToken.Application.Config;
public interface IAuth0M2MSettings
{
    public string? ClientId { get; }
    public string? ServerDomain { get; }
    public string? Audience { get; }
    public string? ClientSecret { get; }
}
