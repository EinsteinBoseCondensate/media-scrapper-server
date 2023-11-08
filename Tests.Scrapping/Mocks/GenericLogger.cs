using Common.Extensions;
using Microsoft.Extensions.Logging;

namespace Tests.Scrapping.Mocks;
public class GenericLogger<T> : ILogger<T> where T : class
{
    private readonly string Category = typeof(T).SafeName();
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        var message = formatter(state, exception);
        if (string.IsNullOrEmpty(message) && exception == null)
            return;
        LogUsingException(logLevel, state, message, exception!);
    }

    private void LogUsingException<TState>(LogLevel logLevel, TState state, string message, Exception exception)
    {
        if (exception == null)
        {
            Console.WriteLine($"[{Category}] {logLevel} - {message}");
        }
        else
        {
            Console.WriteLine($"[{Category}] {logLevel} - {message} - {exception.GetContent()}");
        }
    }
}
