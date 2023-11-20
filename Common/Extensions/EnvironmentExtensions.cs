using Microsoft.Extensions.Hosting;

namespace Common.Extensions;
public static class EnvironmentExtensions
{
    public static string? GetEnvironmentVariableBasedOnOperativeSystem(this string environmentVariableName)
    {
        return OperatingSystem.IsWindows() ?
                    Environment.GetEnvironmentVariable(Constants.ASPNETCORE_ENVIRONMENT) == Environments.Development ?
                        Environment.GetEnvironmentVariable(environmentVariableName, EnvironmentVariableTarget.User) :
                        Environment.GetEnvironmentVariable(environmentVariableName, EnvironmentVariableTarget.Machine) :
                Environment.GetEnvironmentVariable(environmentVariableName);
    }
}
