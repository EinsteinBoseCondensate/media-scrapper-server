using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Common.Logging;

public static class IoC
{
    public static IServiceCollection AddLoggerProvider(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerProvider, Log4netLoggerProvider>();
        return services;
    }
}
