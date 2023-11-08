using log4net;
using log4net.Core;
using Microsoft.Extensions.Logging;

namespace Common.Logging;

public class Log4NetLogger : Microsoft.Extensions.Logging.ILogger
{
    private const string DefaultLogger = "Default";
    private readonly ILog _log;


    public Log4NetLogger(string name, string loggerRepositoryName)
    {
        _log = LogManager.Exists(loggerRepositoryName, name);
        if (_log != null)
            return;
        _log = LogManager.GetLogger(loggerRepositoryName, DefaultLogger);
        _log.Warn($"Log {name} not present in config file, it's used default instead");

    }

    public IDisposable? BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => _log.Logger.IsEnabledFor(Level.Trace) || _log.IsDebugEnabled,
            LogLevel.Debug => _log.IsDebugEnabled,
            LogLevel.Information => _log.IsInfoEnabled,
            LogLevel.Warning => _log.IsWarnEnabled,
            LogLevel.Error => _log.IsErrorEnabled,
            LogLevel.Critical => _log.IsFatalEnabled,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel)),
        };
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;
        if (formatter == null)
            throw new ArgumentNullException(nameof(formatter));
        string message = null;
        if (formatter != null)
            message = formatter(state, exception);
        if (string.IsNullOrEmpty(message) && exception == null)
            return;
        LogUsingException(logLevel, state, message, exception);
    }

    private void LogUsingException<TState>(LogLevel logLevel, TState state, string message, Exception exception)
    {
        if (exception == null)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _log.Logger.Log(typeof(TState), Level.Trace, message, exception);
                    break;
                case LogLevel.Debug:
                    _log.Debug(message);
                    break;
                case LogLevel.Information:
                    _log.Info(message);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message);
                    break;
                case LogLevel.Error:
                    _log.Error(message);
                    break;
                case LogLevel.Critical:
                    _log.Fatal(message);
                    break;
                default:
                    _log.Warn(string.Format("Encountered unknown log level {0}, writing out as Info.", logLevel));
                    _log.Info(message, exception);
                    break;
            }
        }
        else
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _log.Logger.Log(typeof(TState), Level.Trace, message, exception);
                    break;
                case LogLevel.Debug:
                    _log.Debug(message, exception);
                    break;
                case LogLevel.Information:
                    _log.Info(message, exception);
                    break;
                case LogLevel.Warning:
                    _log.Warn(message, exception);
                    break;
                case LogLevel.Error:
                    _log.Error(message, exception);
                    break;
                case LogLevel.Critical:
                    _log.Fatal(message, exception);
                    break;
                default:
                    _log.Warn(string.Format("Encountered unknown log level {0}, writing out as Info.", logLevel));
                    _log.Info(message, exception);
                    break;
            }
        }
    }

    /// <summary>To String Method</summary>
    /// <returns>Formatted string</returns>
    public override string ToString()
    {
        return _log.Logger.Name + " " + (!_log.IsDebugEnabled || _log.Logger.IsEnabledFor(Level.Trace) ? _log.Logger.IsEnabledFor(Level.Trace) ? "TRACE" : _log.IsInfoEnabled ? "INFO" : _log.IsWarnEnabled ? "WARN" : _log.IsErrorEnabled ? "ERROR" : "FATAL" : "DEBUG");
    }
}